using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Hangfire;
using Newtonsoft.Json;
using Serilog;
using WarframeMarketAggregator.Interfaces;
using WarframeMarketAggregator.Models;

namespace WarframeMarketAggregator.App_Start
{
    public class CacheBootstrapper
    {
        public static void Bootstrap(IItemCacheService itemCacheService)
        {
            RefreshItems(itemCacheService);

            RecurringJob.AddOrUpdate(() => RefreshItems(itemCacheService, true), Cron.Daily);
        }

        public static void RefreshItems(IItemCacheService itemCacheService, bool forceRefresh = false)
        {
            Task.Run(async () =>
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
                    itemCacheService.InitializeCache(dataset);
                    Log.Information("Item Cache Loaded");
                }
                else
                {
                    var items = await itemCacheService.GetItems(forceRefresh: true);
                    var json = JsonConvert.SerializeObject(items);
                    using (var writer = new StreamWriter(itemCacheFilePath))
                    {
                        writer.Write(json);
                    }
                    Log.Information("Items Loaded");
                }
            });
        }
    }
}
