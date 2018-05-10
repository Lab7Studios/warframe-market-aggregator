using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WarframeMarketAggregator.App_Start;
using WarframeMarketAggregator.Interfaces;

namespace WarframeMarketAggregator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMemoryCache();
            services.AddHangfire(x => x.UseMemoryStorage());

            services.Configure<ServiceSettings>(Configuration.GetSection("ServiceSettings"));

            ContainerConfig.Configure(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() {Title = "Warframe Market Aggregator", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime, IOptions<ServiceSettings> serviceSettings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SerilogConfig.Configure(serviceSettings);

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warframe Market Aggregator v1");
            });

            app.UseHangfireServer();

            applicationLifetime.ApplicationStarted.Register(() => 
                CacheBootstrapper.Bootstrap(app.ApplicationServices.GetService<IItemCacheService>()));
        }
    }
}
