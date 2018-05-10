using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using WarframeMarketAggregator.Interfaces;
using WarframeMarketAggregator.Models;

namespace WarframeMarketAggregator.Services
{
    public class WarframeMarketService : IMarketService
    {
        private readonly string _baseUrl;

        public WarframeMarketService(IOptions<ServiceSettings> serviceSettings)
        {
            _baseUrl = $"{serviceSettings.Value.WarframeMarketBaseUrl}/items";
        }

        public async Task<IEnumerable<ItemManifest>> GetItemManifest()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_baseUrl);
                response.EnsureSuccessStatusCode();
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                return JObjectToObject<IEnumerable<ItemManifest>>(jObject["payload"]["items"]["en"]);
            }
        }

        public async Task<IEnumerable<ItemInSet>> GetItemsInSet(string itemUrlName)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_baseUrl}/{itemUrlName}");
                response.EnsureSuccessStatusCode();
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                return JObjectToObject<IEnumerable<ItemInSet>>(jObject["payload"]["item"]["items_in_set"]);
            }
        }

        public async Task<ItemStatistic> GetItemStatistic(string itemUrlName)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_baseUrl}/{itemUrlName}/statistics");
                response.EnsureSuccessStatusCode();
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                var statistics = JObjectToObject<IEnumerable<ItemStatistic>>(jObject["payload"]["statistics"]["90days"]).ToList();
                statistics.Sort((stat1, stat2) => stat2.DateTime.CompareTo(stat1.DateTime));
                return statistics.FirstOrDefault();
            }
        }

        public async Task<ItemWithStatistics> GetItemWithStatistic(string itemUrlName)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_baseUrl}/{itemUrlName}/statistics?include=item");
                response.EnsureSuccessStatusCode();
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                var items = JObjectToObject<IEnumerable<ItemWithStatistics>>(jObject["include"]["item"]["items_in_set"])
                    .ToList();
                var targetItem = items.First(item => item.UrlName == itemUrlName);
                var statistics = JObjectToObject<IEnumerable<ItemStatistic>>(jObject["payload"]["statistics"]["90days"])
                    .ToList();
                statistics.Sort((stat1, stat2) => stat2.DateTime.CompareTo(stat1.DateTime));
                targetItem.Statistics = statistics.FirstOrDefault();
                return targetItem;
            }
        }

        private static T JObjectToObject<T>(JToken jToken)
        {
            return jToken.ToObject<T>(new JsonSerializer()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            });
        }
    }
}
