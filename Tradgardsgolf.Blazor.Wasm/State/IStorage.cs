using System.Threading.Tasks;

namespace Tradgardsgolf.Blazor.Wasm.State
{
    public interface IStorage
    {
        ValueTask<T> GetAsync<T>(string key);

        ValueTask SetAsync(string key, object value);
    }
}
