using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CacheManager.Core.Internal;

namespace CacheManager.Core
{
    public partial interface ICacheManager<TCacheValue>
    {
#if !NET40
        /// <summary>
        /// Adds an item to the cache or, if the item already exists, updates the item using the
        /// <paramref name="updateValue"/> function.
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
        /// <param name="addValue">
        /// The value which should be added in case the item doesn't already exist.
        /// </param>
        /// <param name="updateValue">
        /// The function to perform the update in case the item does already exist.
        /// </param>
        /// <returns>
        /// The value which has been added or updated, or null, if the update was not successful.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="updateValue"/> are null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        ValueTask<TCacheValue> AddOrUpdateAsync(string key, TCacheValue addValue, Func<TCacheValue, TCacheValue> updateValue);

        /// <summary>
        /// Adds an item to the cache or, if the item already exists, updates the item using the
        /// <paramref name="updateValue"/> function.
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
        /// <param name="region">The region of the key to update.</param>
        /// <param name="addValue">
        /// The value which should be added in case the item doesn't already exist.
        /// </param>
        /// <param name="updateValue">
        /// The function to perform the update in case the item does already exist.
        /// </param>
        /// <returns>
        /// The value which has been added or updated, or null, if the update was not successful.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="region"/> or <paramref name="updateValue"/>
        /// are null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        ValueTask<TCacheValue> AddOrUpdateAsync(string key, string region, TCacheValue addValue, Func<TCacheValue, TCacheValue> updateValue);

        /// <summary>
        /// Adds an item to the cache or, if the item already exists, updates the item using the
        /// <paramref name="updateValue"/> function.
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
        /// <param name="addValue">
        /// The value which should be added in case the item doesn't already exist.
        /// </param>
        /// <param name="updateValue">
        /// The function to perform the update in case the item does already exist.
        /// </param>
        /// <param name="maxRetries">
        /// The number of tries which should be performed in case of version conflicts.
        /// If the cache cannot perform an update within the number of <paramref name="maxRetries"/>,
        /// this method will return <c>Null</c>.
        /// </param>
        /// <returns>
        /// The value which has been added or updated, or null, if the update was not successful.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="updateValue"/> is null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        ValueTask<TCacheValue> AddOrUpdateAsync(string key, TCacheValue addValue, Func<TCacheValue, TCacheValue> updateValue, int maxRetries);

        /// <summary>
        /// Adds an item to the cache or, if the item already exists, updates the item using the
        /// <paramref name="updateValue"/> function.
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
        /// <param name="region">The region of the key to update.</param>
        /// <param name="addValue">
        /// The value which should be added in case the item doesn't already exist.
        /// </param>
        /// <param name="updateValue">
        /// The function to perform the update in case the item does already exist.
        /// </param>
        /// <param name="maxRetries">
        /// The number of tries which should be performed in case of version conflicts.
        /// If the cache cannot perform an update within the number of <paramref name="maxRetries"/>,
        /// this method will return <c>Null</c>.
        /// </param>
        /// <returns>
        /// The value which has been added or updated, or null, if the update was not successful.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="region"/> or <paramref name="updateValue"/> is null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        ValueTask<TCacheValue> AddOrUpdateAsync(string key, string region, TCacheValue addValue, Func<TCacheValue, TCacheValue> updateValue, int maxRetries);

        /// <summary>
        /// Adds an item to the cache or, if the item already exists, updates the item using the
        /// <paramref name="updateValue"/> function.
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
        /// <param name="addItem">The item which should be added or updated.</param>
        /// <param name="updateValue">The function to perform the update, if the item does exist.</param>
        /// <returns>
        /// The value which has been added or updated, or null, if the update was not successful.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="addItem"/> or <paramref name="updateValue"/> are null.
        /// </exception>
        ValueTask<TCacheValue> AddOrUpdateAsync(CacheItem<TCacheValue> addItem, Func<TCacheValue, TCacheValue> updateValue);

        /// <summary>
        /// Adds an item to the cache or, if the item already exists, updates the item using the
        /// <paramref name="updateValue"/> function.
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
        /// <param name="addItem">The item which should be added or updated.</param>
        /// <param name="updateValue">The function to perform the update, if the item does exist.</param>
        /// <param name="maxRetries">
        /// The number of tries which should be performed in case of version conflicts.
        /// If the cache cannot perform an update within the number of <paramref name="maxRetries"/>,
        /// this method will return <c>Null</c>.
        /// </param>
        /// <returns>
        /// The value which has been added or updated, or null, if the update was not successful.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="addItem"/> or <paramref name="updateValue"/> is null.
        /// </exception>
        ValueTask<TCacheValue> AddOrUpdateAsync(CacheItem<TCacheValue> addItem, Func<TCacheValue, TCacheValue> updateValue, int maxRetries);

        /// <summary>
        /// Returns an existing item or adds the item to the cache if it does not exist.
        /// The method returns either the existing item's value or the newly added <paramref name="value"/>.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The value which should be added.</param>
        /// <returns>Either the added or the existing value.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/> or <paramref name="value"/> is null.
        /// </exception>
        ValueTask<TCacheValue> GetOrAddAsync(string key, TCacheValue value);

        /// <summary>
        /// Returns an existing item or adds the item to the cache if it does not exist.
        /// The method returns either the existing item's value or the newly added <paramref name="value"/>.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="region">The cache region.</param>
        /// <param name="value">The value which should be added.</param>
        /// <returns>Either the added or the existing value.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/>, <paramref name="region"/> or <paramref name="value"/> is null.
        /// </exception>
        ValueTask<TCacheValue> GetOrAddAsync(string key, string region, TCacheValue value);

        /// <summary>
        /// Returns an existing item or adds the item to the cache if it does not exist.
        /// The <paramref name="valueFactory"/> will be evaluated only if the item does not exist.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="valueFactory">The method which creates the value which should be added.</param>
        /// <returns>Either the added or the existing value.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/> or <paramref name="valueFactory"/> is null.
        /// </exception>
        ValueTask<TCacheValue> GetOrAddAsync(string key, Func<string, TCacheValue> valueFactory);

        /// <summary>
        /// Returns an existing item or adds the item to the cache if it does not exist.
        /// The <paramref name="valueFactory"/> will be evaluated only if the item does not exist.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="region">The cache region.</param>
        /// <param name="valueFactory">The method which creates the value which should be added.</param>
        /// <returns>Either the added or the existing value.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/> or <paramref name="valueFactory"/> is null.
        /// </exception>
        ValueTask<TCacheValue> GetOrAddAsync(string key, string region, Func<string, string, TCacheValue> valueFactory);

        /// <summary>
        /// Returns an existing item or adds the item to the cache if it does not exist.
        /// The <paramref name="valueFactory"/> will be evaluated only if the item does not exist.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="valueFactory">The method which creates the value which should be added.</param>
        /// <returns>Either the added or the existing value.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/> or <paramref name="valueFactory"/> is null.
        /// </exception>
        ValueTask<CacheItem<TCacheValue>> GetOrAddAsync(string key, Func<string, CacheItem<TCacheValue>> valueFactory);

        /// <summary>
        /// Returns an existing item or adds the item to the cache if it does not exist.
        /// The <paramref name="valueFactory"/> will be evaluated only if the item does not exist.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="region">The cache region.</param>
        /// <param name="valueFactory">The method which creates the value which should be added.</param>
        /// <returns>Either the added or the existing value.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/> or <paramref name="valueFactory"/> is null.
        /// </exception>
        ValueTask<CacheItem<TCacheValue>> GetOrAddAsync(string key, string region, Func<string, string, CacheItem<TCacheValue>> valueFactory);

        /// <summary>
        /// Tries to either retrieve an existing item or add the item to the cache if it does not exist.
        /// The <paramref name="valueFactory"/> will be evaluated only if the item does not exist.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="valueFactory">The method which creates the value which should be added.</param>
        /// <returns><c>True</c> and cache value if the operation succeeds, <c>False</c> in case there are too many retries or the <paramref name="valueFactory"/> returns null.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/> or <paramref name="valueFactory"/> is null.
        /// </exception>
        ValueTask<(bool, TCacheValue)> TryGetOrAddAsync(string key, Func<string, TCacheValue> valueFactory);

        /// <summary>
        /// Tries to either retrieve an existing item or add the item to the cache if it does not exist.
        /// The <paramref name="valueFactory"/> will be evaluated only if the item does not exist.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="region">The cache region.</param>
        /// <param name="valueFactory">The method which creates the value which should be added.</param>
        /// <returns><c>True</c> and cache value if the operation succeeds, <c>False</c> in case there are too many retries or the <paramref name="valueFactory"/> returns null.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/> or <paramref name="valueFactory"/> is null.
        /// </exception>
        ValueTask<(bool, TCacheValue)> TryGetOrAddAsync(string key, string region, Func<string, string, TCacheValue> valueFactory);

        /// <summary>
        /// Tries to either retrieve an existing item or add the item to the cache if it does not exist.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="valueFactory">The method which creates the value which should be added.</param>
        /// <returns><c>True</c> and cache item if the operation succeeds, <c>False</c> in case there are too many retries or the <paramref name="valueFactory"/> returns null.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/> or <paramref name="valueFactory"/> is null.
        /// </exception>
        ValueTask<(bool, CacheItem<TCacheValue>)> TryGetOrAddAsync(string key, Func<string, CacheItem<TCacheValue>> valueFactory);

        /// <summary>
        /// Tries to either retrieve an existing item or add the item to the cache if it does not exist.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="region">The cache region.</param>
        /// <param name="valueFactory">The method which creates the value which should be added.</param>
        /// <returns><c>True</c> and cache item if the operation succeeds, <c>False</c> in case there are too many retries or the <paramref name="valueFactory"/> returns null.</returns>
        /// <exception cref="ArgumentException">
        /// If either <paramref name="key"/> or <paramref name="valueFactory"/> is null.
        /// </exception>
        ValueTask<(bool, CacheItem<TCacheValue>)> TryGetOrAddAsync(string key, string region, Func<string, string, CacheItem<TCacheValue>> valueFactory);

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
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        /// <param name="key">The key to update.</param>
        /// <param name="updateValue">The function to perform the update.</param>
        /// <returns>The updated value, or null, if the update was not successful.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="updateValue"/> are null.
        /// </exception>
        ValueTask<TCacheValue> UpdateAsync(string key, Func<TCacheValue, TCacheValue> updateValue);

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
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        /// <param name="key">The key to update.</param>
        /// <param name="region">The region of the key to update.</param>
        /// <param name="updateValue">The function to perform the update.</param>
        /// <returns>The updated value, or null, if the update was not successful.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="region"/> or <paramref name="updateValue"/>
        /// are null.
        /// </exception>
        ValueTask<TCacheValue> UpdateAsync(string key, string region, Func<TCacheValue, TCacheValue> updateValue);

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
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        /// <param name="key">The key to update.</param>
        /// <param name="updateValue">The function to perform the update.</param>
        /// <param name="maxRetries">
        /// The number of tries which should be performed in case of version conflicts.
        /// If the cache cannot perform an update within the number of <paramref name="maxRetries"/>,
        /// this method will return <c>Null</c>.
        /// </param>
        /// <returns>The updated value, or null, if the update was not successful.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="updateValue"/> is null.
        /// </exception>
        ValueTask<TCacheValue> UpdateAsync(string key, Func<TCacheValue, TCacheValue> updateValue, int maxRetries);

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
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        /// <param name="key">The key to update.</param>
        /// <param name="region">The region of the key to update.</param>
        /// <param name="updateValue">The function to perform the update.</param>
        /// <param name="maxRetries">
        /// The number of tries which should be performed in case of version conflicts.
        /// If the cache cannot perform an update within the number of <paramref name="maxRetries"/>,
        /// this method will return <c>Null</c>.
        /// </param>
        /// <returns>The updated value, or null, if the update was not successful.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="region"/> or <paramref name="updateValue"/> is null.
        /// </exception>
        ValueTask<TCacheValue> UpdateAsync(string key, string region, Func<TCacheValue, TCacheValue> updateValue, int maxRetries);

        /// <summary>
        /// Tries to update an existing key in the cache.
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
        /// <returns><c>True</c> and updated value if the update operation was successful, <c>False</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="updateValue"/> are null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        ValueTask<(bool, TCacheValue)> TryUpdateAsync(string key, Func<TCacheValue, TCacheValue> updateValue);

        /// <summary>
        /// Tries to update an existing key in the cache.
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
        /// <param name="region">The region of the key to update.</param>
        /// <param name="updateValue">The function to perform the update.</param>
        /// <returns><c>True</c> and updated value if the update operation was successful, <c>False</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="region"/> or <paramref name="updateValue"/>
        /// are null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        ValueTask<(bool, TCacheValue)> TryUpdateAsync(string key, string region, Func<TCacheValue, TCacheValue> updateValue);

        /// <summary>
        /// Tries to update an existing key in the cache.
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
        /// <param name="maxRetries">
        /// The number of tries which should be performed in case of version conflicts.
        /// If the cache cannot perform an update within the number of <paramref name="maxRetries"/>,
        /// this method will return <c>False</c>.
        /// </param>
        /// <returns><c>True</c> and updated value if the update operation was successful, <c>False</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="updateValue"/> is null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        ValueTask<(bool, TCacheValue)> TryUpdateAsync(string key, Func<TCacheValue, TCacheValue> updateValue, int maxRetries);

        /// <summary>
        /// Tries to update an existing key in the cache.
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
        /// <param name="region">The region of the key to update.</param>
        /// <param name="updateValue">The function to perform the update.</param>
        /// <param name="maxRetries">
        /// The number of tries which should be performed in case of version conflicts.
        /// If the cache cannot perform an update within the number of <paramref name="maxRetries"/>,
        /// this method will return <c>False</c>.
        /// </param>
        /// <returns><c>True</c> and updated value if the update operation was successful, <c>False</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="key"/> or <paramref name="region"/> or <paramref name="updateValue"/> is null.
        /// </exception>
        /// <remarks>
        /// If the cache does not use a distributed cache system. Update is doing exactly the same
        /// as Get plus Put.
        /// </remarks>
        ValueTask<(bool, TCacheValue)> TryUpdateAsync(string key, string region, Func<TCacheValue, TCacheValue> updateValue, int maxRetries);

        /// <summary>
        /// Explicitly sets the expiration <paramref name="mode"/> and <paramref name="timeout"/> for the
        /// <paramref name="key"/> in all cache layers.
        /// This operation overrides any configured expiration per cache handle for this particular item.
        /// </summary>
        /// <remarks>
        /// Don't use this in concurrency critical scenarios if you are using distributed caches as <code>Expire</code> is not atomic;
        /// <code>Expire</code> uses <see cref="ICache{TCacheValue}.Put(CacheItem{TCacheValue})"/> to store the item with the new expiration.
        /// </remarks>
        /// <param name="key">The cache key.</param>
        /// <param name="mode">The expiration mode.</param>
        /// <param name="timeout">The expiration timeout.</param>
        ValueTask ExpireAsync(string key, ExpirationMode mode, TimeSpan timeout);

        /// <summary>
        /// Explicitly sets the expiration <paramref name="mode"/> and <paramref name="timeout"/> for the
        /// <paramref name="key"/> in <paramref name="region"/> in all cache layers.
        /// This operation overrides any configured expiration per cache handle for this particular item.
        /// </summary>
        /// <remarks>
        /// Don't use this in concurrency critical scenarios if you are using distributed caches as <code>Expire</code> is not atomic;
        /// <code>Expire</code> uses <see cref="ICache{TCacheValue}.Put(CacheItem{TCacheValue})"/> to store the item with the new expiration.
        /// </remarks>
        /// <param name="key">The cache key.</param>
        /// <param name="region">The cache region.</param>
        /// <param name="mode">The expiration mode.</param>
        /// <param name="timeout">The expiration timeout.</param>
        ValueTask ExpireAsync(string key, string region, ExpirationMode mode, TimeSpan timeout);

        /// <summary>
        /// Explicitly sets an absolute expiration date for the <paramref name="key"/> in all cache layers.
        /// This operation overrides any configured expiration per cache handle for this particular item.
        /// </summary>
        /// <remarks>
        /// Don't use this in concurrency critical scenarios if you are using distributed caches as <code>Expire</code> is not atomic;
        /// <code>Expire</code> uses <see cref="ICache{TCacheValue}.Put(CacheItem{TCacheValue})"/> to store the item with the new expiration.
        /// </remarks>
        /// <param name="key">The cache key.</param>
        /// <param name="absoluteExpiration">
        /// The expiration date. The value must be greater than zero.
        /// </param>
        ValueTask ExpireAsync(string key, DateTimeOffset absoluteExpiration);

        /// <summary>
        /// Explicitly sets an absolute expiration date for the <paramref name="key"/> in <paramref name="region"/> in all cache layers.
        /// This operation overrides any configured expiration per cache handle for this particular item.
        /// </summary>
        /// <remarks>
        /// Don't use this in concurrency critical scenarios if you are using distributed caches as <code>Expire</code> is not atomic;
        /// <code>Expire</code> uses <see cref="ICache{TCacheValue}.Put(CacheItem{TCacheValue})"/> to store the item with the new expiration.
        /// </remarks>
        /// <param name="key">The cache key.</param>
        /// <param name="region">The cache region.</param>
        /// <param name="absoluteExpiration">
        /// The expiration date. The value must be greater than zero.
        /// </param>
        ValueTask ExpireAsync(string key, string region, DateTimeOffset absoluteExpiration);

        /// <summary>
        /// Explicitly sets a sliding expiration date for the <paramref name="key"/> in all cache layers.
        /// This operation overrides any configured expiration per cache handle for this particular item.
        /// </summary>
        /// <remarks>
        /// Don't use this in concurrency critical scenarios if you are using distributed caches as <code>Expire</code> is not atomic;
        /// <code>Expire</code> uses <see cref="ICache{TCacheValue}.Put(CacheItem{TCacheValue})"/> to store the item with the new expiration.
        /// </remarks>
        /// <param name="key">The cache key.</param>
        /// <param name="slidingExpiration">
        /// The expiration timeout. The value must be greater than zero.
        /// </param>
        ValueTask ExpireAsync(string key, TimeSpan slidingExpiration);

        /// <summary>
        /// Explicitly sets a sliding expiration date for the <paramref name="key"/> in <paramref name="region"/> in all cache layers.
        /// This operation overrides any configured expiration per cache handle for this particular item.
        /// </summary>
        /// <remarks>
        /// Don't use this in concurrency critical scenarios if you are using distributed caches as <code>Expire</code> is not atomic;
        /// <code>Expire</code> uses <see cref="ICache{TCacheValue}.Put(CacheItem{TCacheValue})"/> to store the item with the new expiration.
        /// </remarks>
        /// <param name="key">The cache key.</param>
        /// <param name="region">The cache region.</param>
        /// <param name="slidingExpiration">
        /// The expiration timeout. The value must be greater than zero.
        /// </param>
        ValueTask ExpireAsync(string key, string region, TimeSpan slidingExpiration);

        /// <summary>
        /// Explicitly removes any expiration settings previously defined for the <paramref name="key"/>
        /// in all cache layers and sets the expiration mode to <see cref="ExpirationMode.None"/>.
        /// This operation overrides any configured expiration per cache handle for this particular item.
        /// </summary>
        /// <remarks>
        /// Don't use this in concurrency critical scenarios if you are using distributed caches as <code>Expire</code> is not atomic;
        /// <code>Expire</code> uses <see cref="ICache{TCacheValue}.Put(CacheItem{TCacheValue})"/> to store the item with the new expiration.
        /// </remarks>
        /// <param name="key">The cache key.</param>
        ValueTask RemoveExpirationAsync(string key);

        /// <summary>
        /// Explicitly removes any expiration settings previously defined for the <paramref name="key"/> in <paramref name="region"/>
        /// in all cache layers and sets the expiration mode to <see cref="ExpirationMode.None"/>.
        /// This operation overrides any configured expiration per cache handle for this particular item.
        /// </summary>
        /// <remarks>
        /// Don't use this in concurrency critical scenarios if you are using distributed caches as <code>Expire</code> is not atomic;
        /// <code>Expire</code> uses <see cref="ICache{TCacheValue}.Put(CacheItem{TCacheValue})"/> to store the item with the new expiration.
        /// </remarks>
        /// <param name="key">The cache key.</param>
        /// <param name="region">The cache region.</param>
        ValueTask RemoveExpirationAsync(string key, string region);
#endif
    }
}
