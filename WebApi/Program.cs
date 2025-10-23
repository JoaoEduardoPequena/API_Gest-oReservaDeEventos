using System.Text.Json.Serialization;
using Application;
using Persistence;
using WebApi.Configuration;
using WebApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORS",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddMvc()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfraPersistenceServices(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHsts(opt =>
{
    opt.Preload = true;
    opt.IncludeSubDomains = true;
});


var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(swaggerUI =>
    {
        swaggerUI.SwaggerEndpoint("/swagger/reservas/swagger.json", "Reservas");
        swaggerUI.SwaggerEndpoint("/swagger/eventos/swagger.json", "Eventos");
    });
}
app.MapSwagger();
app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
app.UseCors("CORS");
app.UseExceptionHandler();
app.MapControllers();
app.Run();
