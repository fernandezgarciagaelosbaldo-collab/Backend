using Microsoft.EntityFrameworkCore;
using SistemaProduccionMVC.Models;

namespace SistemaProduccionMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Conexión a SQL Server (SmarterASP)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<SistemaProduccionContext>(options =>
                options.UseSqlServer(connectionString));

            // 2. Activar controladores API
            builder.Services.AddControllers();

            // 3. Configurar CORS (por ahora abierto para pruebas)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPermitirTodo", policy =>
                {
                    policy.AllowAnyOrigin()      //  solo para pruebas
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // 4. Middleware básico
            app.UseHttpsRedirection();

            // 5. Activar CORS
            app.UseCors("CorsPermitirTodo");

            // 6. Routing
            app.UseRouting();

            app.UseAuthorization();

           //Mapear solo controladores API
            app.MapControllers();

            app.Run();
        }
    }
}

