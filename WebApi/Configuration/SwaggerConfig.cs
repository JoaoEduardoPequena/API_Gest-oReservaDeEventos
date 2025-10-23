using Microsoft.OpenApi.Models;

namespace WebApi.Configuration
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(d =>
            {
                d.SwaggerDoc("reservas", new OpenApiInfo
                {
                    Title = "Web API Gestão de Reservas de Eventos",
                    Version = "v1",
                    Description =
                        "Documentaçao Referente api para gestão de reservas de eventos",
                });

                d.SwaggerDoc("eventos", new OpenApiInfo
                {
                    Title = "Web API Gestão de Eventos",
                    Version = "v1",
                    Description =
                        "Documentaçao Referente api para gestão de eventos",
                });

            });
        }
    }
}
