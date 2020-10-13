using System.Threading.Tasks;

namespace Tradgardsgolf.Blazor.State
{
    public interface IStorage
    {
        ValueTask<T> GetAsync<T>(string key);

        ValueTask SetAsync(string key, object value);
    }
}
