using Microsoft.Extensions.DependencyInjection;
using System;
using Vini.ModelProject.Infra.Data.Repositories;

namespace Vini.ModelProject.Infra.CrossCutting.DI
{
    public class NativeInjectorBootstraper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<UsuárioRepository>();

        }
    }
}
