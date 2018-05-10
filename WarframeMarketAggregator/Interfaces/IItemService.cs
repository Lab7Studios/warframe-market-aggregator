using System.Collections.Generic;
using System.Threading.Tasks;
using WarframeMarketAggregator.Models;

namespace WarframeMarketAggregator.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemWithStatistics>> GetItems();
    }
}
