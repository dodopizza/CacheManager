using System;
using System.Linq;
using System.Threading.Tasks;
using static CacheManager.Core.Utility.Guard;

namespace CacheManager.Core
{
    public partial class BaseCacheManager<TCacheValue>
    {
#if !NET40
        /// <inheritdoc />
        public ValueTask<TCacheValue> GetOrAddAsync(string key, TCacheValue value)
            => GetOrAddAsync(key, (k) => value);

        /// <inheritdoc />
        public ValueTask<TCacheValue> GetOrAddAsync(string key, string region, TCacheValue value)
            => GetOrAddAsync(key, region, (k, r) => value);

        /// <inheritdoc />
        public async ValueTask<TCacheValue> GetOrAddAsync(string key, Func<string, TCacheValue> valueFactory)
        {
            NotNullOrWhiteSpace(key, nameof(key));
            NotNull(valueFactory, nameof(valueFactory));

            return (await GetOrAddInternalAsync(key, null, (k, r) => new CacheItem<TCacheValue>(k, valueFactory(k)))).Value;
        }

        /// <inheritdoc />
        public async ValueTask<TCacheValue> GetOrAddAsync(string key, string region, Func<string, string, TCacheValue> valueFactory)
        {
            NotNullOrWhiteSpace(key, nameof(key));
            NotNullOrWhiteSpace(region, nameof(region));
            NotNull(valueFactory, nameof(valueFactory));

            return (await GetOrAddInternalAsync(key, region, (k, r) => new CacheItem<TCacheValue>(k, r, valueFactory(k, r)))).Value;
        }

        /// <inheritdoc />
        public ValueTask<CacheItem<TCacheValue>> GetOrAddAsync(string key, Func<string, CacheItem<TCacheValue>> valueFactory)
        {
            NotNullOrWhiteSpace(key, nameof(key));
            NotNull(valueFactory, nameof(valueFactory));

            return GetOrAddInternalAsync(key, null, (k, r) => valueFactory(k));
        }

        /// <inheritdoc />
        public ValueTask<CacheItem<TCacheValue>> GetOrAddAsync(string key, string region, Func<string, string, CacheItem<TCacheValue>> valueFactory)
        {
            NotNullOrWhiteSpace(key, nameof(key));
            NotNullOrWhiteSpace(region, nameof(region));
            NotNull(valueFactory, nameof(valueFactory));

            return GetOrAddInternalAsync(key, region, valueFactory);
        }

        /// <inheritdoc />
        public async ValueTask<(bool, TCacheValue)> TryGetOrAddAsync(string key, Func<string, TCacheValue> valueFactory)
        {
            NotNullOrWhiteSpace(key, nameof(key));
            NotNull(valueFactory, nameof(valueFactory));

            TCacheValue value;

            var (hasItem, item) = await TryGetOrAddInternalAsync(key,
                null,
                (k, r) =>
                {
                    var newValue = valueFactory(k);
                    return newValue == null ? null : new CacheItem<TCacheValue>(k, newValue);
                });
            if (hasItem)
            {
                value = item.Value;
                return (true, value);
            }

            value = default(TCacheValue);
            return (false, value);
        }

        /// <inheritdoc />
        public async ValueTask<(bool, TCacheValue)> TryGetOrAddAsync(string key, string region, Func<string, string, TCacheValue> valueFactory)
        {
            NotNullOrWhiteSpace(key, nameof(key));
            NotNullOrWhiteSpace(region, nameof(region));
            NotNull(valueFactory, nameof(valueFactory));

            TCacheValue value;
            var (hasItem, item) = await TryGetOrAddInternalAsync(key,
                region,
                (k, r) =>
                {
                    var newValue = valueFactory(k, r);
                    return newValue == null ? null : new CacheItem<TCacheValue>(k, r, newValue);
                });
            if (hasItem)
            {
                value = item.Value;
                return (true, value);
            }

            value = default(TCacheValue);
            return (false, value);
        }

        /// <inheritdoc />
        public ValueTask<(bool, CacheItem<TCacheValue>)> TryGetOrAddAsync(string key, Func<string, CacheItem<TCacheValue>> valueFactory)
        {
            NotNullOrWhiteSpace(key, nameof(key));
            NotNull(valueFactory, nameof(valueFactory));

            return TryGetOrAddInternalAsync(key, null, (k, r) => valueFactory(k));
        }

        /// <inheritdoc />
        public ValueTask<(bool, CacheItem<TCacheValue>)> TryGetOrAddAsync(string key, string region, Func<string, string, CacheItem<TCacheValue>> valueFactory)
        {
            NotNullOrWhiteSpace(key, nameof(key));
            NotNullOrWhiteSpace(region, nameof(region));
            NotNull(valueFactory, nameof(valueFactory));

            return TryGetOrAddInternalAsync(key, region, valueFactory);
        }

        private async ValueTask<(bool, CacheItem<TCacheValue>)> TryGetOrAddInternalAsync(string key, string region, Func<string, string, CacheItem<TCacheValue>> valueFactory)
        {
            CacheItem<TCacheValue> item;
            CacheItem<TCacheValue> newItem = null;
            
            var tries = 0;
            do
            {
                tries++;
                item = await GetCacheItemInternalAsync(key, region);
                if (item != null)
                {
                    return (true, item);
                }

                // changed logic to invoke the factory only once in case of retries
                if (newItem == null)
                {
                    newItem = valueFactory(key, region);
                }

                if (newItem == null)
                {
                    return (false, item);
                }

                if (await AddInternalAsync(newItem))
                {
                    item = newItem;
                    return (true, item);
                }
            }
            while (tries <= Configuration.MaxRetries);

            return (false, item);
        }

        private async ValueTask<CacheItem<TCacheValue>> GetOrAddInternalAsync(string key, string region, Func<string, string, CacheItem<TCacheValue>> valueFactory)
        {
            CacheItem<TCacheValue> newItem = null;
            var tries = 0;
            do
            {
                tries++;
                var item = await GetCacheItemInternalAsync(key, region);
                if (item != null)
                {
                    return item;
                }

                // changed logic to invoke the factory only once in case of retries
                if (newItem == null)
                {
                    newItem = valueFactory(key, region);
                }

                // Throw explicit to me more consistent. Otherwise it would throw later eventually...
                if (newItem == null)
                {
                    throw new InvalidOperationException("The CacheItem which should be added must not be null.");
                }

                if (await AddInternalAsync(newItem))
                {
                    return newItem;
                }
            }
            while (tries <= Configuration.MaxRetries);

            // should usually never occur, but could if e.g. max retries is 1 and an item gets added between the get and add.
            // pretty unusual, so keep the max tries at least around 50
            throw new InvalidOperationException(
                string.Format("Could not get nor add the item {0} {1}", key, region));
        }
#endif
    }
}
