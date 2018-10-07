using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Vini.ModelProject.Api.Configurations
{
    public static class LocalizationConfiguration
    {
        public static void AddLocalizationCustom(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("pt-BR", "pt-BR");

                options.SupportedCultures =
                options.SupportedUICultures = new[]
                {
                    new CultureInfo("pt-BR"),
                    new CultureInfo("es")
                };
            });
        }

        public static void AddLocalizationToRequest(this IApplicationBuilder app)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
        }
    }
}
