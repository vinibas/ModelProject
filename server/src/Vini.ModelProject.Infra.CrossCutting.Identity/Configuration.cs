﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Vini.ModelProject.Infra.CrossCutting.Identity.Data;
using Vini.ModelProject.Infra.CrossCutting.Identity.Models;

namespace Vini.ModelProject.Infra.CrossCutting.Identity
{
    public static class Configuration
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<UsuárioIdentity, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

        }
    }
}
