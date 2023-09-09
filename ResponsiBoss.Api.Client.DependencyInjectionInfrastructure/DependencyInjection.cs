using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Client.DependencyInjectionInfrastructure
{
    public static class DependencyInjection
    {
        public static void Init(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure settings for IOptions injection.
            services.Configure<Options.AppOptions>(options =>
            {
                options.ApiBaseUrl = configuration.GetSection("AppSettings:ApiBaseUrl").Value;
                options.AllowUntrustedCert = bool.Parse(configuration.GetSection("AppSettings:AllowUntrustedCert").Value);
            });

            // Using Scrutor To Register All Clients.
            services.Scan(scan =>
                scan.FromAssemblyOf<UserClient>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Client")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }
    }
}