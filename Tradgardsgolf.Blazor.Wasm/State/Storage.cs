using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Tradgardsgolf.Blazor.Wasm.State
{
    public class Storage : IStorage
    {
        private readonly ProtectedSessionStorage _storage;
        private readonly ILogger _logger;

        public Storage(ProtectedSessionStorage storage, ILogger<Storage> logger)
        {
            _storage = storage;
            _logger = logger;
        }

        public async ValueTask<T> GetAsync<T>(string key)
        {
            try
            {
                return await _storage.GetAsync<T>(key);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return default(T);
        }

        public ValueTask SetAsync(string key, object value)
        {
            try
            {
                return _storage.SetAsync(key, value);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return new ValueTask();
        }
    }

    public class ProtectedSessionStorage
    {
        private static IDictionary<string, object> Store = new Dictionary<string, object>();
        
        
        public ValueTask SetAsync(string key, object value)
        {
            if (Store.ContainsKey(key))
                Store[key] = value;
            else
                Store.Add(key, value);

            return ValueTask.CompletedTask;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (Store.TryGetValue(key, out var value) && value is T result)
                return await Task.FromResult(result);

            return default(T);
        }
    }
}
