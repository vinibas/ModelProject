using Microsoft.Extensions.DependencyInjection;
using System;
using Vini.ModelProject.Infra.Data.Repositories;

namespace Vini.ModelProject.Infra.CrossCutting.DI
{
    public static class NativeInjectorBootstraper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<UsuárioRepository>();

        }
    }
}
