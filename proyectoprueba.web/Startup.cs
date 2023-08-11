using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using proyectoprueba.datos;
using proyectoprueba.servicios.repositorios.PersonaRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyectoprueba.web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
       // readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "proyectoprueba.web", Version = "v1" });
            });
            services.AddScoped<PersonaRepo>();
            services.AddDbContext<AppDbContext>(options =>
           {
                options.UseSqlServer(Configuration.GetConnectionString("Conexion")).LogTo(Console.WriteLine, LogLevel.Information);
            });
            //services.AddCors(options =>
            //{
                //    options.AddPolicy("AllowSpecificOrigin", builder =>
                //    {
                //        var corsSettings = Configuration.GetSection("CorsSettings");
                //        var allowSpecificOrigin = corsSettings["AllowSpecificOrigin"];
                //        var allowAnyHeader = Convert.ToBoolean(corsSettings["AllowAnyHeader"]);
                //        var allowAnyMethod = Convert.ToBoolean(corsSettings["AllowAnyMethod"]);

                //        builder.WithOrigins(allowSpecificOrigin)
                //            .AllowAnyHeader()
                //            .AllowAnyMethod();
                //    });
                //});

                services.AddCors(options => options.AddPolicy("AllowebApp",
                    builder => builder.WithOrigins("https://localhost",
                  "https://localhost:44327",
                  "https://localhost:8080").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "proyectoprueba.web v1"));
            }
            app.UseCors("AllowebApp");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
