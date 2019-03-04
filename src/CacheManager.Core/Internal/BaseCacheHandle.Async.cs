using System;
using System.Threading.Tasks;
using CacheManager.Core.Logging;
using static CacheManager.Core.Utility.Guard;

namespace CacheManager.Core.Internal
{
#if !NET40
    public abstract partial class BaseCacheHandle<TCacheValue>
    {
        /// <summary>
        /// Updates an existing key in the cache.
        /// <para>
        /// The cache manager will make sure the update will always happen on the most recent version.
        /// </para>
        /// <para>
        /// If version conflicts occur, if for example multiple cache clients try to write the same
        /// key, and during the update process, someone else changed the value for the key, the
        /// cache manager will retry the operation.
        /// </para>
        /// <para>
        /// The <paramref name="updateValue"/> function will get invoked on each retry with the most
        /// recent value which is stored in cache.
        /// </para>
        /// </summary>
        /// <param name="key">The key to update.</param>
        /// <param name="updateValue">The function to perform the update.</param>
        /// <param name="maxRetries">The number of tries.</param>
        /// <returns>The update result which is interpreted by the cache manager.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="updateValue"/> is null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        public virtual async ValueTask<UpdateItemResult<TCacheValue>> UpdateAsync(string key, Func<TCacheValue, TCacheValue> updateValue, int maxRetries)
        {
            NotNull(updateValue, nameof(updateValue));
            CheckDisposed();

            var original = await GetCacheItemAsync(key);
            if (original == null)
            {
                return UpdateItemResult.ForItemDidNotExist<TCacheValue>();
            }

            var newValue = updateValue(original.Value);

            if (newValue == null)
            {
                return UpdateItemResult.ForFactoryReturnedNull<TCacheValue>();
            }

            var newItem = original.WithValue(newValue);
            newItem.LastAccessedUtc = DateTime.UtcNow;
            await PutAsync(newItem);
            return UpdateItemResult.ForSuccess(newItem);
        }

        /// <summary>
        /// Updates an existing key in the cache.
        /// <para>
        /// The cache manager will make sure the update will always happen on the most recent version.
        /// </para>
        /// <para>
        /// If version conflicts occur, if for example multiple cache clients try to write the same
        /// key, and during the update process, someone else changed the value for the key, the
        /// cache manager will retry the operation.
        /// </para>
        /// <para>
        /// The <paramref name="updateValue"/> function will get invoked on each retry with the most
        /// recent value which is stored in cache.
        /// </para>
        /// </summary>
        /// <param name="key">The key to update.</param>
        /// <param name="region">The cache region.</param>
        /// <param name="updateValue">The function to perform the update.</param>
        /// <param name="maxRetries">The number of tries.</param>
        /// <returns>The update result which is interpreted by the cache manager.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/>, <paramref name="region"/> or <paramref name="updateValue"/> is null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        public virtual async ValueTask<UpdateItemResult<TCacheValue>> UpdateAsync(string key, string region, Func<TCacheValue, TCacheValue> updateValue, int maxRetries)
        {
            NotNull(updateValue, nameof(updateValue));
            CheckDisposed();

            // TODO: fix lock for async
            //lock (_updateLock)
            {
                var original = await GetCacheItemAsync(key, region);
                if (original == null)
                {
                    return UpdateItemResult.ForItemDidNotExist<TCacheValue>();
                }

                var newValue = updateValue(original.Value);
                if (newValue == null)
                {
                    return UpdateItemResult.ForFactoryReturnedNull<TCacheValue>();
                }

                var newItem = original.WithValue(newValue);

                newItem.LastAccessedUtc = DateTime.UtcNow;
                await PutAsync(newItem);
                return UpdateItemResult.ForSuccess(newItem);
            }
        }
        
        /// <inheritdoc />
        protected internal override ValueTask<bool> AddInternalAsync(CacheItem<TCacheValue> item)
        {
            CheckDisposed();
            item = GetItemExpiration(item);
            return AddInternalPreparedAsync(item);
        }

        /// <summary>
        /// Adds a value to the cache.
        /// </summary>
        /// <param name="item">The <c>CacheItem</c> to be added to the cache.</param>
        /// <returns>
        /// <c>true</c> if the key was not already added to the cache, <c>false</c> otherwise.
        /// </returns>
        protected virtual ValueTask<bool> AddInternalPreparedAsync(CacheItem<TCacheValue> item)
        {
            var result = AddInternalPrepared(item);
            return new ValueTask<bool>(result);
        }

        /// <summary>
        /// Clears this cache, removing all items in the base cache and all regions.
        /// </summary>
        public override ValueTask ClearAsync()
        {
            Clear();
            return new ValueTask();
        }

        /// <summary>
        /// Clears the cache region, removing all items from the specified <paramref name="region"/> only.
        /// </summary>
        /// <param name="region">The cache region.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="region"/> is null.</exception>
        public override ValueTask ClearRegionAsync(string region)
        {
            ClearRegion(region);
            return new ValueTask();
        }

        /// <inheritdoc />
        public override ValueTask<bool> ExistsAsync(string key)
        {
            var result = Exists(key);
            return new ValueTask<bool>(result);
        }

        /// <inheritdoc />
        public override ValueTask<bool> ExistsAsync(string key, string region)
        {
            var result = Exists(key, region);
            return new ValueTask<bool>(result);
        }

        /// <inheritdoc />
        protected override ValueTask<CacheItem<TCacheValue>> GetCacheItemInternalAsync(string key)
        {
            var result = GetCacheItemInternal(key);
            return new ValueTask<CacheItem<TCacheValue>>(result);
        }

        /// <inheritdoc />
        protected override ValueTask<CacheItem<TCacheValue>> GetCacheItemInternalAsync(string key, string region)
        {
            var result = GetCacheItemInternal(key, region);
            return new ValueTask<CacheItem<TCacheValue>>(result);
        }
        
        /// <summary>
        /// Puts the <paramref name="item"/> into the cache. If the item exists it will get updated
        /// with the new value. If the item doesn't exist, the item will be added to the cache.
        /// </summary>
        /// <param name="item">The <c>CacheItem</c> to be added to the cache.</param>
        protected internal override ValueTask PutInternalAsync(CacheItem<TCacheValue> item)
        {
            CheckDisposed();
            item = GetItemExpiration(item);
            return PutInternalPreparedAsync(item);
        }

        /// <summary>
        /// Puts the <paramref name="item"/> into the cache. If the item exists it will get updated
        /// with the new value. If the item doesn't exist, the item will be added to the cache.
        /// </summary>
        /// <param name="item">The <c>CacheItem</c> to be added to the cache.</param>
        protected virtual ValueTask PutInternalPreparedAsync(CacheItem<TCacheValue> item)
        {
            PutInternalPrepared(item);
            return new ValueTask();
        }
        
        /// <inheritdoc />
        protected override ValueTask<bool> RemoveInternalAsync(string key)
        { 
            var result = RemoveInternal(key);
            return new ValueTask<bool>(result);
        }

        /// <inheritdoc />
        protected override ValueTask<bool> RemoveInternalAsync(string key, string region)
        {
            var result = RemoveInternal(key, region);
            return new ValueTask<bool>(result);
        }
    }
#endif
}
