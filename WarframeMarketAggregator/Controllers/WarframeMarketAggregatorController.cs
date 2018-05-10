using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarframeMarketAggregator.Interfaces;
using WarframeMarketAggregator.Models;

namespace WarframeMarketAggregator.Controllers
{
    [Route("api/v1")]
    public class WarframeMarketAggregatorControllerV1 : Controller
    {
        private readonly IItemCacheService _itemCacheService;

        public WarframeMarketAggregatorControllerV1(IItemCacheService itemCacheService)
        {
            _itemCacheService = itemCacheService;
        }

        [HttpGet("manifest")]
        public async Task<IEnumerable<string>> GetItemManifest()
        {
            return (await _itemCacheService.GetItems()).Select(item => item.UrlName);
        }

	    [HttpGet("items")]
	    public async Task<IEnumerable<ItemWithStatistics>> GetCompleteDataset()
	    {
		    return await _itemCacheService.GetItems();
	    }

		[HttpGet("items/{itemUrlName}")]
        public async Task<ItemInSet> GetItem(string itemUrlName)
        {
            return (await _itemCacheService.GetItems()).First(item => item.UrlName == itemUrlName);
        }

        [HttpGet("items/{itemUrlName}/statistics")]
        public async Task<ItemStatistic> GetItemStatistic(string itemUrlName)
        {
            return (await _itemCacheService.GetItems()).First(item => item.UrlName == itemUrlName)?.Statistics;
        }
    }
}
