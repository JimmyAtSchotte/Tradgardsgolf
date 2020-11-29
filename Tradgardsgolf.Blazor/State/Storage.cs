﻿using Microsoft.AspNetCore.ProtectedBrowserStorage;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Tradgardsgolf.Blazor.State
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
}