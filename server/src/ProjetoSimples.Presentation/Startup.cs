using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjetoSimples.Presentation.Data;
using ProjetoSimples.Presentation.Models;
using System;
using System.Globalization;
using System.Text;

namespace ProjetoSimples.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            
            services.AddTransient<UsuárioRepository>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            if (Configuration.GetValue<bool>("UseWebApi"))
                ConfigureServicesToApi(services);
            else
                ConfigureServicesToMvc(services);
            
            services.AddOptions();
            services.AddMvc(options => options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter()));


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

        public void ConfigureServicesToMvc(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
                options.LoginPath = options.LogoutPath = "/Conta/Login");

            services.AddMvc()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.SubFolder)
                .AddDataAnnotationsLocalization();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });
        }

        public void ConfigureServicesToApi(IServiceCollection services)
        {
            //services
            //    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateLifetime = false,
            //            ValidateIssuerSigningKey = false,
            //            ValidIssuer = "https://localhost:44367/",
            //            ValidAudience = "http://localhost:4200/",
            //            IssuerSigningKey = new SymmetricSecurityKey(
            //                Encoding.UTF8.GetBytes("SecuritySecretKey"))
            //        };
            //    });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
            {
                options.Audience = "http://localhost:4200/";
                options.ClaimsIssuer = "https://localhost:44367/";
                options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = "https://localhost:44367/",
                        ValidAudience = "http://localhost:4200/",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("SecuritySecretKey"))
                    }; 
                options.SaveToken = true;
            });

            //services.AddAuthorization(auth =>
            //{
            //    auth.AddPolicy("Bearer", new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //});


            //services.AddAuthentication(op =>
            //{
            //    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(bearerOptions =>
            //{
            //    bearerOptions.RequireHttpsMetadata = false;
            //    bearerOptions.SaveToken = true;

            //    var paramsValidation = bearerOptions.TokenValidationParameters;

            //    paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecuritySecretKey"));
            //    paramsValidation.ValidAudience = "https://localhost";
            //    paramsValidation.ValidIssuer = "https://localhost";

            //    paramsValidation.ValidateIssuerSigningKey = true;
            //    paramsValidation.ValidateLifetime = true;
            //    paramsValidation.ClockSkew = System.TimeSpan.Zero;
            //});


            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var locOptions = app.ApplicationServices.GetService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (Configuration.GetValue<bool>("UseWebApi"))
                ConfigureToApi(app, env);
            else
                ConfigureToMvc(app, env);

            app.UseStaticFiles();
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureToApi(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(p => p
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        }

        private void ConfigureToMvc(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseSession();
        }
    }
}
