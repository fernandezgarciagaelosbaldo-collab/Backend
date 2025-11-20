using Microsoft.EntityFrameworkCore;
using SistemaProduccionMVC.Models; // <- esto se agregará después del scaffold

namespace SistemaProduccionMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Obtener cadena de conexión
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // 2. Agregar DbContext al contenedor
            builder.Services.AddDbContext<SistemaProduccionContext>(options =>
                options.UseSqlServer(connectionString));
       
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

