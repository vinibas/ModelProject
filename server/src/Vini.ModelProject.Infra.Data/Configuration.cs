using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vini.ModelProject.Infra.Data
{
    public static class Configuration
    {
        public static void ConfigureData(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ModelProjectContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
