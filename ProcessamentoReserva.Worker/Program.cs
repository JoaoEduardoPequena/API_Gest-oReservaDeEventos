using ProcessamentoReserva.Worker;
using ProcessamentoReserva.Worker.Workers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddNotificadorPedidosWorker(builder.Configuration);
builder.Services.AddHostedService<SubscribeMessageWork>();

var host = builder.Build();
host.Run();
