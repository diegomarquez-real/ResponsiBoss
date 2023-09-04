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
        }
    }
}