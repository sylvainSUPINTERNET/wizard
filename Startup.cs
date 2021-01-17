using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

using config.DbClient;
using models.Profiles;
using services.ProfileServices;

namespace ApiPpc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DbClient DbClient = new DbClient();
            
            var collection = DbClient.GetCollection("users");
            Profiles newProfile = new Profiles("1234", "sylvain");

            ProfileServices ps = new ProfileServices();
            ps.insertOne(collection, newProfile.toBsonDocument());
        
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPpc", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiPpc v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.Use( (context, next) => {
                // custom middleware
                return next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/custom", async context => {
                    Console.WriteLine("-- TEST --");

                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    //string msg = await new Test().GetMessage();

                    // anonymous obj
                    var student = new { Id = 1, FirstName = "James", LastName = "Bond", Lyric="msg" };
                    
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(student));
                });


            });
        }
    }
}
