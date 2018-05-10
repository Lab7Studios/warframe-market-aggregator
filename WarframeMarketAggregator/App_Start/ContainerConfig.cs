using Microsoft.Extensions.DependencyInjection;
using WarframeMarketAggregator.Interfaces;
using WarframeMarketAggregator.Services;

namespace WarframeMarketAggregator.App_Start
{
    public class ContainerConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IMarketService, WarframeMarketService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddSingleton<IItemCacheService, ItemCacheService>();
        }
    }
}
