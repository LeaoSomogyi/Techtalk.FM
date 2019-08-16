using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Techtalk.FM.API.Filters;
using Techtalk.FM.Domain.Configurations;
using Techtalk.FM.Domain.Contracts.Migrations;
using Techtalk.FM.Domain.Contracts.Repositories;
using Techtalk.FM.Domain.Contracts.Services;
using Techtalk.FM.Domain.DTOs.Validators;
using Techtalk.FM.Domain.Services;
using Techtalk.FM.Infra.Repositories.NHibernate;
using Techtalk.FM.Infra.Repositories.NHibernate.Migrations;

namespace Techtalk.FM.API
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
            #region "  Configure MVC and JSON Serialize options  "

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };

                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
                    opt.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-US");
                    opt.SerializerSettings.Formatting = Formatting.None;
                    opt.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
                    opt.SerializerSettings.FloatParseHandling = FloatParseHandling.Double;
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>()); ;

            #endregion

            #region "  Configure Dependency Injection  "

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMigrationHelper, MigrationHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILoginService, LoginService>();

            var tokenConfig = new TokenConfigurations();
            Configuration.Bind("TokenConfigurations", tokenConfig);

            var signingConfigurations = new SigningConfigurations();

            services.AddSingleton(tokenConfig);
            services.AddSingleton(signingConfigurations);

            #endregion

            #region "  Configure one transation per HTTP Call  "

            services.AddScoped((serviceProvider) =>
            {
                var wow = serviceProvider.GetService<IUnitOfWork>();

                wow.Open();

                return wow.BeginTransaction();
            });

            services.AddScoped(typeof(UnitOfWorkFilter));

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.AddService<UnitOfWorkFilter>(1);
            });

            #endregion

            #region "  Configure API Result  "

            services.AddScoped(typeof(APIResultFilter));

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.AddService<APIResultFilter>(2);
            });

            #endregion

            #region "  Configure Auth  "

            ConfigureAuth(services, signingConfigurations);

            #endregion
        }

        private void ConfigureAuth(IServiceCollection services, SigningConfigurations signingConfigurations)
        {
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {
                bearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingConfigurations.SecurityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMigrationHelper migrationService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            migrationService.RunMigrationUp();
        }
    }
}
