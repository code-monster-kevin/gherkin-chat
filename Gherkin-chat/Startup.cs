using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gherkin_chat.Entities;
using Gherkin_chat.DTO;
using Gherkin_chat.Repositories;
using Gherkin_chat.SignalRHub;
using System.Reflection;
using System.IO;

namespace Gherkin_chat
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// IConfigurationRoot Configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }
        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        /// <summary>
        /// Startup(IHostingEnvironment environment)
        /// </summary>
        /// <param name="environment"></param>
        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// ConfigureServices(IServiceCollection services)
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:7335");
                });
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Gherkin Chat API",
                    Version = "V1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<GherkinDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GherkinChatDB")));
            services.AddScoped<IMessageRepositories, MessageRepositories>();
            services.AddMvc();
            services.AddSignalR().AddAzureSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configure(IApplicationBuilder app, IHostingEnvironment env)
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gherkin Chat API");
            });

            app.UseDefaultFiles();
            app.UseFileServer();
            app.UseAzureSignalR(routes =>
            {
                routes.MapHub<MessageHub>("/messagehub");
            });
            app.UseMvc();
        }
    }
}
