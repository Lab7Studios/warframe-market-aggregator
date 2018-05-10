using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;
using WarframeMarketAggregator.Interfaces;
using WarframeMarketAggregator.Models;

namespace WarframeMarketAggregator.Services
{
    public class CacheFileManager : ICacheFileManager
    {
	    private readonly IItemCacheService _itemCacheService;

	    public CacheFileManager(IItemCacheService itemCacheService)
	    {
		    _itemCacheService = itemCacheService;
	    }

	    public async Task RefreshItems(bool forceRefresh = false)
        {
            const string itemCacheFilePath = "./item-cache.json";
            if (!forceRefresh && File.Exists(itemCacheFilePath))
            {
                string json;
                using (var reader = new StreamReader(itemCacheFilePath))
                {
                    json = reader.ReadToEnd();
                }
                var dataset = JsonConvert.DeserializeObject<IEnumerable<ItemWithStatistics>>(json);
                _itemCacheService.InitializeCache(dataset);
                Log.Information("Item Cache Loaded");
            }
            else
            {
                var items = await _itemCacheService.GetItems(forceRefresh: true);
                var json = JsonConvert.SerializeObject(items);
                using (var writer = new StreamWriter(itemCacheFilePath))
                {
                    writer.Write(json);
                }
                Log.Information("Items Loaded");
            }
        }
    }
}
