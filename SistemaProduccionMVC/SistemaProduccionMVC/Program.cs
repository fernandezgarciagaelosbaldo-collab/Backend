using Microsoft.EntityFrameworkCore;
using SistemaProduccionMVC.Models;

namespace SistemaProduccionMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Conexión a SQL Server (local o SmarterASP)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ProduccionDbContext>(options =>
                options.UseSqlServer(connectionString));

            // 2. Activar controladores API
            builder.Services.AddControllers();

            // 3. Configurar CORS (abierto para desarrollo)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPermitirTodo", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // 4. Middleware esencial
            app.UseHttpsRedirection();

            // 5. Habilitar CORS
            app.UseCors("CorsPermitirTodo");

            app.UseRouting();

            // 6. Autorización (si después agregas JWT seguirá funcionando)
            app.UseAuthorization();

            // 7. Mapear solo controladores API
            app.MapControllers();

            app.Run();
        }
    }
}
