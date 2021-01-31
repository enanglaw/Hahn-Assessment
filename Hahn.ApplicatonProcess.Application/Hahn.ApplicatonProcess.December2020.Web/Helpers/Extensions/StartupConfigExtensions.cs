using Hahn.ApplicatonProcess.December2020.Data.Core;
using Hahn.ApplicatonProcess.December2020.Data.Repositories;
using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Helpers.Extensions
{
    public static class StartupConfigExtensions
    {

        public static CorsOptions ConfigureCorsPolicy(this CorsOptions corsOptions)
        {
            corsOptions.AddPolicy("AllowAll",
                                  corsPolicyBuilder => corsPolicyBuilder
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowAnyOrigin()
                                 );
            return corsOptions;
        }

        public static SwaggerGenOptions ConfigureSwagger(this SwaggerGenOptions options)
        {
            options.SwaggerDoc("Hanhs_Applicantion_process_API_v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Hanhs Applicantion Process API v1",
                Version = "v1",
                Description = "Hanhs Applicantion Process API v1"
            });
            options.ResolveConflictingActions((description) => description.First());
            return options;
        }


        public static SwaggerUIOptions ConfigureSwaggerUI(this SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/Hanhs_Applicantion_process_API_v1/swagger.json", "Hanhs Applicantion Process API Docs");
            options.RoutePrefix = string.Empty;
            return options;
        }

        public static IServiceCollection ConfigureApplicationCoreServices(this IServiceCollection services)
        {

            services.AddScoped<IApplicantService, ApplicantService>();
            return services;
        }

        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //data services
            services.AddScoped<IAppLogger, AppLoger>();

            services.AddDbContext<AppDbContext>();

            services.AddEntityFrameworkSqlite();

            services.AddScoped<IRepository<Applicant>, BaseEFRepository<Applicant>>();
            return services;
        }

    }
}
