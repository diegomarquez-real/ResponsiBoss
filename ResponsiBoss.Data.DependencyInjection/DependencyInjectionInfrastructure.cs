using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.DependencyInjection
{
    public class DependencyInjectionInfrastructure
    {
        public static void ImplementPersisence(IServiceCollection services)
        {
            services.AddScoped<Abstractions.IDataContext, DataContext>();
        }
    }
}