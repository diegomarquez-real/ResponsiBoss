using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.DependencyInjectionInfrastructure
{
    public static class DependencyInjection
    {
        public static void Init(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure settings for IOptions injection.
            services.Configure<Options.ConnectionStringOptions>(options => configuration.GetSection("ConnectionStrings"));

            services.AddScoped<Abstractions.IDataContext, DataContext>();

            // Using Scrutor to register all repositories.
            services.Scan(scan =>
                scan.FromAssemblyOf<UserRepository>() // The actual repoository here doesn't matter, just using it to find which assembly our repositories are in.
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }
    }
}