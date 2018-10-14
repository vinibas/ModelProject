using Microsoft.Extensions.DependencyInjection;
using System;
using Vini.ModelProject.Application.AplicationServices;
using Vini.ModelProject.Application.Interfaces;
using Vini.ModelProject.Domain.Interfaces.Repositories;
using Vini.ModelProject.Domain.Interfaces.Services;
using Vini.ModelProject.Domain.Services;
using Vini.ModelProject.Infra.CrossCutting.Identity.Services;
using Vini.ModelProject.Infra.Data.Repositories;

namespace Vini.ModelProject.Infra.CrossCutting.DI
{
    public static class NativeInjectorBootstraper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            RegisterApplicationServices(services);
            RegisterDomainServices(services);
            RegisterIdentityServices(services);
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IContaAppService, ContaAppService>();
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddTransient<IUsuárioRepository, UsuárioRepository>();
            services.AddTransient<IUsuárioService, UsuárioService>();
        }

        private static void RegisterIdentityServices(IServiceCollection services)
        {
            services.AddTransient<IContaIdentityService, ContaIdentityService>();

            //services.AddScoped<Microsoft.AspNetCore.Identity.IdentityUser, Identity.Models.UsuárioIdentity> ();
            //services.AddScoped<Vini.ModelProject.Infra.CrossCutting.Identity.Data.IdentityDbContext>();

        }
    }
}
