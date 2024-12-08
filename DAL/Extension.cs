using DAL.DataServices;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class Extension
    {
        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IOfficesDAL, OfficesDAL>();
            services.AddTransient<IRolesDAL, RolesDAL>();
            services.AddTransient<IEmployeesDAL, EmployeesDAL>();
            var serviceProvider = services.BuildServiceProvider();

            try
            {
                // Test connection & run migrations
                using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                Console.WriteLine("Database successfully migrated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
