using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Application.Setting;
using Application.Interfaces.RabbitMQ;
using Application.Services.RabbitMQ;
using MassTransit;
using Application.Interfaces;
using Application.Services;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration conf)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            services.Configure<FaceioSetting>(op => conf.GetSection("FaceioSetting").Bind(op));
            services.AddStackExchangeRedisCache(op => op.Configuration = conf["RedisSetting:ConnectionString"]);
            services.AddSingleton<IRedisService, RedisService>();
            services.Configure<RabbitMqSetting>(op => conf.GetSection("RabbitMqSetting").Bind(op));
            services.AddTransient<IMessageBroker,MessageBroker>();
            services.AddMassTransit(configs =>
            {
                configs.UsingRabbitMq((context, config) =>
                {
                    config.Host(conf.GetValue<string>("RabbitMqSetting:HostName"), h =>
                    {
                        h.Username(conf.GetValue<string>("RabbitMqSetting:UserName"));
                        h.Password(conf.GetValue<string>("RabbitMqSetting:PassWord"));
                    });
                });
            });
            return services;
        }

    }
}
