using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using WarframeMarketAggregator.Interfaces;
using WarframeMarketAggregator.Models;

namespace WarframeMarketAggregator.Services
{
    public class ItemService : IItemService
    {
        private readonly IMarketService _marketService;

        public ItemService(IMarketService marketService)
        {
            _marketService = marketService;
        }

        public async Task<IEnumerable<ItemWithStatistics>> GetItems()
        {
            var itemManifest = await _marketService.GetItemManifest();
            var items = new List<ItemWithStatistics>();

            foreach (var manifest in itemManifest)
            {
                var item = await _marketService.GetItemWithStatistic(manifest.UrlName);
                if (!item.SetRoot)
                {
                    items.Add(item);
                    Log.Debug($"Fetched item: '{item.UrlName}'");
                }
            }

            return items.AsEnumerable();
        }
    }
}
