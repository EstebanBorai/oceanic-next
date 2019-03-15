using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ContactsDirectory.Core;
using ContactsDirectory.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ContactsDirectory.API
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
            services.AddCors();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                services.AddDbContext<ContactsDirectoryContext>(options =>
                options.UseSqlServer(Configuration.GetSection("Database").GetValue<string>("SqlServer"),
                    builder => builder.MigrationsAssembly("ContactsDirectory.API"))
                );
            }
            else
            {
                services.AddDbContext<ContactsDirectoryContext>(options =>
                options.UseSqlite(Configuration.GetSection("Database").GetValue<string>("Sqlite"),
                    builder => builder.MigrationsAssembly("ContactsDirectory.API"))
                );
            }

            services.AddScoped<IDataSource>(ds => new ContactsDirectoryDataSource(ds.GetRequiredService<ContactsDirectoryContext>()));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "Contacts Directory API",
                    Description = "Contacts Directory API made with .NET Core",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Esteban Borai",
                        Email = string.Empty,
                        Url = "https://github.com/estebanborai"
                    },
                    License = new License
                    {
                        Name = "Use under MIT License",
                        Url = "https://opensource.org/licenses/MIT"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts Directory API V1");
            });

            app.UseMvc();
        }
    }
}
