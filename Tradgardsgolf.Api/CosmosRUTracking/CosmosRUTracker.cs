using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tradgardsgolf.Api.CosmosRUTracking;

public partial class CosmosRUTracker
{
    private readonly ConcurrentDictionary<string, List<string>> _requestEntries = new();
    
    public void Log(string entry, string traceId)
    {
        if (!_requestEntries.TryGetValue(traceId, out var entries))
            entries = [];
        
        entries.Add(entry);

        _requestEntries[traceId] = entries;
    }

    public IEnumerable<RuUsage> TotalCharge(string traceId)
    {
        var regex = RuRegex();
        
        if(!_requestEntries.TryGetValue(traceId, out var requestEntry))
            return [];

        return requestEntry
                    .Select(entry => regex.Match(entry))
                    .Where(match => match.Success)
                    .Select(match => new RuUsage
                    {
                        DateTime = ToDateTime(match.Groups["Date"].Value),
                        Ru = ToDouble(match.Groups["RU"].Value)
                    })
                    .GroupBy(x => x.DateTime, x => x.Ru)
                    .Select(x => new RuUsage
                    {
                        DateTime = x.Key,
                        Ru = x.Sum()
                    });
    }

    private static DateTime ToDateTime(string value)
    {
        return DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp) 
            ? timestamp 
            : DateTime.MinValue;
    }

    private static double ToDouble(string value)
    {
        value = value.Replace(",", ".");
            
        // Parse as double using InvariantCulture to ensure dot as decimal separator
        return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedRU) ? parsedRU : 0;


    }
    

    [GeneratedRegex(@"(?<Date>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})\.\d{3}[\s\S]*?\([\d\.,]+\s+ms,\s+(?<RU>[\d\.,]+)\s+RU\)")]
    private static partial Regex RuRegex();
}

public class RuUsage
{
    public double Ru { get; init; }
    public DateTime DateTime { get; init; }
}