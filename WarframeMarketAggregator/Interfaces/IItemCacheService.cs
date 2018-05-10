using System.Collections.Generic;
using System.Threading.Tasks;
using WarframeMarketAggregator.Models;

namespace WarframeMarketAggregator.Interfaces
{
    public interface IItemCacheService
    {
        Task<IEnumerable<ItemWithStatistics>> GetItems(bool forceRefresh = false);

        void InitializeCache(IEnumerable<ItemWithStatistics> items);
    }
}
