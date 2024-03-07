using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Control14.NetCore.BusinessLogic;
using Control14.NetCore.DataAccess;
using Control14.NetCore.Domain;
using Control14.NetCore.Domain.Interfaces;
using Control14.NetCore.BusinessLogic.Services;
using Control14.NetCore.DataAccess.Repositories;

namespace Control14.NetCore.Api
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            var cultureInfo = new CultureInfo("es-PE");
            cultureInfo.NumberFormat.CurrencySymbol = "S/.";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<E_ConnectionStrings>(options =>
            {
                options.Sql = Configuration.GetConnectionString("ConnectionSQL");
                //options.PostgreSql = Configuration.GetConnectionString("ConnectionCanvas");
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins(Configuration["Settings:OriginDomainLocal"].ToString()
                        , Configuration["Settings:OriginDomainProduction"].ToString()
                        , Configuration["Settings:OriginDomainProduction2"].ToString())
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });

 



            services.AddScoped<IPeriodsRepository, PeriodsRepository>();
            services.AddScoped<PeriodsService>();

            services.AddScoped<IVehiculosRepository, VehiculosRepository>();
            services.AddScoped<VehiculosService>();

            services.AddScoped<ICiudadesRepository, CiudadesRepository>();
            services.AddScoped<CiudadesService>();

            services.AddScoped<IViajeRepository, ViajeRepository>();
            services.AddScoped<ViajeService>();


            services.AddLogging(builder =>
            {
                builder.AddConsole(); // Adds console logging
            });
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/{Date}.txt");

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
