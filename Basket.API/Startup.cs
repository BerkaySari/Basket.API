using Data.Models.Mapping.Consumers;
using Data.Models.Mapping.Mapper;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Basket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region redis
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });
            #endregion

            #region mass transit - rabbit
            services.AddMassTransit(x =>
            {
                //x.AddConsumer<CreateOrderConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration.GetValue<string>("RabbitMqSettings:Host"), hst =>
                    {
                        hst.Username(Configuration.GetValue<string>("RabbitMqSettings:Username"));
                        hst.Password(Configuration.GetValue<string>("RabbitMqSettings:Password"));
                    });

                    cfg.ReceiveEndpoint("createorderevent", e =>
                    {
                        //e.ConfigureConsumer<CreateOrderConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddMassTransitHostedService();

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(MapperProfile));
            #endregion

            #region services
            services.AddTransient<IBasketService, BasketService>();
            #endregion

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
