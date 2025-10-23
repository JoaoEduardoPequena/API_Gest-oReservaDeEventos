using Infrastruture.Worker;
using MassTransit;
using ProcessamentoReserva.Worker.Consumers;
using ProcessamentoReserva.Worker.Interfaces;
using ProcessamentoReserva.Worker.Process;

namespace ProcessamentoReserva.Worker
{
    public static class WorkerServiceRegistration
    {
        public static void AddNotificadorPedidosWorker(this IServiceCollection services, IConfiguration config)
        {
            services.AddInfrastructureWorker(config);
            services.AddMassTransit(configs =>
            {
                configs.AddConsumer<PedidosReservasConsumer>();

                configs.UsingRabbitMq((context, conf) =>
                {
                    conf.Host(config.GetValue<string>("RabbitMqSetting:HostName"), h =>
                    {
                        h.Username(config.GetValue<string>("RabbitMqSetting:UserName"));
                        h.Password(config.GetValue<string>("RabbitMqSetting:PassWord"));
                    });
                    conf.ReceiveEndpoint(config.GetValue<string>("RabbitMqSetting:QueueReserva"), e =>
                    {
                        e.ConfigureConsumer<PedidosReservasConsumer>(context);
                    });
                    conf.UseRawJsonSerializer();
                    conf.ConfigureEndpoints(context);
                });
            });
            services.AddMassTransitHostedService();
            services.AddSingleton<ISubscribeMessageProcess, SubscribeMessageProcess>();
        }
    }
}
