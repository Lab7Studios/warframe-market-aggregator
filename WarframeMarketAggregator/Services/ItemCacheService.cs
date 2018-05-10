using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using WarframeMarketAggregator.Interfaces;
using WarframeMarketAggregator.Models;

namespace WarframeMarketAggregator.Services
{
    public class ItemCacheService : IItemCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IItemService _itemService;

        private const string ItemCacheKey = "items";

        public ItemCacheService(IMemoryCache cache, IItemService itemService)
        {
            _cache = cache;
            _itemService = itemService;
        }

        public Task<IEnumerable<ItemWithStatistics>> GetItems(bool forceRefresh = false)
        {
            if (!forceRefresh)
            {
                return _cache.GetOrCreate(ItemCacheKey, entry =>
                {
                    var task = Task.Run(async () => await _itemService.GetItems());

                    return task;
                });
            }
            else
            {
                var task = Task.Run(async () => await _itemService.GetItems());

                if (!_cache.TryGetValue(ItemCacheKey, out var _))
                {
                    _cache.CreateEntry(ItemCacheKey);
                }

                task.Wait();

                _cache.Set(ItemCacheKey, task);

                return task;
            }
        }

        public void InitializeCache(IEnumerable<ItemWithStatistics> items)
        {
            _cache.CreateEntry(ItemCacheKey);
            _cache.Set(ItemCacheKey, Task.Run(() => items));
        }
    }
}
