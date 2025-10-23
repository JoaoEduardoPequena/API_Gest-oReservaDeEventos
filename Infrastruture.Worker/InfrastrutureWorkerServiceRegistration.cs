using Infrastruture.Worker.Interfaces;
using Infrastruture.Worker.Services;
using Infrastruture.Worker.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastruture.Worker
{
    public static class InfrastrutureWorkerServiceRegistration
    {
        public static void AddInfrastructureWorker(this IServiceCollection services, IConfiguration conf)
        {
            services.Configure<EmailSetting>(op => conf.GetSection("EmailSetting").Bind(op));
            services.Configure<RedisSetting>(op => conf.GetSection("RedisSetting").Bind(op));
            services.AddSingleton<ISendEmailService, SendEmailService>();
            
            services.AddStackExchangeRedisCache(op => op.Configuration = conf["RedisSetting:ConnectionString"]);
            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(conf["RedisSetting:ConnectionString"]));
            services.AddSingleton<IRedisService, RedisService>();
            services.Configure<RabbitMqSetting>(op => conf.GetSection("RabbitMqSetting").Bind(op));
            services.AddSingleton<IGeneratorReportService, GeneratorReportService>();
        }
    }
}
