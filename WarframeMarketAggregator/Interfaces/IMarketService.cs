using System.Collections.Generic;
using System.Threading.Tasks;
using WarframeMarketAggregator.Models;

namespace WarframeMarketAggregator.Interfaces
{
    public interface IMarketService
    {
        Task<IEnumerable<ItemManifest>> GetItemManifest();

        Task<IEnumerable<Item>> GetItemsInSet(string itemUrlName);

        Task<ItemStatistic> GetItemStatistic(string itemUrlName);

        Task<ItemWithStatistics> GetItemWithStatistic(string itemUrlName);
    }
}
