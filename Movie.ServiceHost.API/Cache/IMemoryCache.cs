using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.Cache
{
    public interface IMemoryCache : IDisposable
    {
        /// <summary>
        /// Cache is there/is not there
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <param name="value">IENumarable<List> data</param>
        /// <returns>True/False</returns>
        bool TryGetValue(object key, out object value);
        /// <summary>
        /// Create Cache
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <returns>ICacheEntery</returns>
        ICacheEntry CreateEntry(object key);
        /// <summary>
        /// Cache Delete
        /// </summary>
        /// <param name="key">Cache Key</param>
        void Remove(object key);
    }
}
