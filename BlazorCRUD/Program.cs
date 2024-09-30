using BlazorCRUD.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlazorCRUD.Data;

namespace BlazorCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Registrar IDbContextFactory para Blazor
            builder.Services.AddDbContextFactory<MyAppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyAppDbContext") ??
                throw new InvalidOperationException("Connection string 'MyAppDbContext' not found.")));

            builder.Services.AddQuickGridEntityFrameworkAdapter();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Añadir servicios para Razor Components y Blazor interactivo
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configurar el pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                app.UseMigrationsEndPoint();  // Mover a la sección de no desarrollo si corresponde.
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            // Mapear Razor Components
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
