using System.Collections.Concurrent;
using System.Globalization;
using System.Text.RegularExpressions;

namespace StatsMigration;

public class CosmosRUTracker
{
    private readonly ConcurrentDictionary<string, List<string>> _requestEntries = new();
    
    public void Log(string entry, string traceId)
    {
        if (!_requestEntries.TryGetValue(traceId, out var entries))
            entries = new List<string>();
        
        entries.Add(entry);

        _requestEntries[traceId] = entries;
    }

    public IEnumerable<RuUsage> TotalCharge(string traceId)
    {
        var regex = new Regex(@"(?<Date>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})\.\d{3}[\s\S]*?\([\d\.,]+\s+ms,\s+(?<RU>[\d\.,]+)\s+RU\)");
        
        if(!_requestEntries.ContainsKey(traceId))
            return [];

        return _requestEntries[traceId]
                    .Select(entry => regex.Match(entry))
                    .Where(match => match.Success)
                    .Select(match => new RuUsage()
                    {
                        DateTime = ToDateTime(match.Groups["Date"].Value),
                        Ru = ToDouble(match.Groups["RU"].Value)
                    })
                    .GroupBy(x => x.DateTime, x => x.Ru)
                    .Select(x => new RuUsage()
                    {
                        DateTime = x.Key,
                        Ru = x.Sum()
                    });
    }

    private DateTime ToDateTime(string value)
    {
        if (DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp))
            return timestamp;

        return DateTime.MinValue;
    }

    private double ToDouble(string value)
    {
        value = value.Replace(",", ".");
            
        // Parse as double using InvariantCulture to ensure dot as decimal separator
        return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedRU) ? parsedRU : 0;


    }

    public void Clear()
    {
        _requestEntries.Clear();
    }
}

public class RuUsage
{
    public double Ru { get; set; }
    public DateTime DateTime { get; set; }
}