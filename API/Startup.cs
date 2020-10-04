using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using FinanceManagement.DataAccess.Context;
using FinanceManagement.DataRepository;
using FinanceManagement.DataAccess;
using Microsoft.OpenApi.Models;
using FinanceManagement.BusinessLogic.Managers;
using FinanceManagement.BusinessLogic.Implementations;
using FinanceManagement.API.Helpers;

namespace FinanceManagementAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<FinanceManagementContext>(options => options.UseMySQL(Configuration.GetConnectionString("Database")));
            services.AddScoped<ICategoriesRepository, CategoriesDataAccess>();
            services.AddScoped<ICategoriesManager, CategoriesManager>();
            services.AddScoped<IFinancialTransactionsRepository, FinancialTransactionsDataAccess>();
            services.AddScoped<IFinancialTransactionsManager, FinancialTransactionsManager>();
            services.AddScoped<IPeriodsRepository, PeriodsDataAccess>();
            services.AddScoped<IPeriodsManager, PeriodsManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Finance Management API",
                    Version = "v1"
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers.Add(new OpenApiServer
                    {
                        Url = $"https://{httpReq.Host.Value}"
                    });
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Finance Management API");
            });

            app.UseMiddleware(typeof(ExceptionHandler));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
