using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Database;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.Repo;
using MvcPeopleDataManagementSPA.Models.Service;

namespace MvcPeopleDataManagementSPA
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //------------------------- connection to database -----------------------------------------
            services.AddDbContext<PeopleDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //------------------------- identity ---------------------------------------------------
            services.AddIdentity<IdentityAppUser, IdentityRole>()
                        .AddEntityFrameworkStores<PeopleDbContext>()
                        .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            //------------------------- services IoC ---------------------------------------------------
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IPersonLanguageService, PersonLanguageService>();

            //------------------------- repo IoC -------------------------------------------------------
            //services.AddSingleton<IPeopleRepo, InMemoryPeopleRepo>();
            services.AddScoped<IPeopleRepo, DatabasePeopleRepo>();
            services.AddScoped<ICityRepo, DatabaseCityRepo>();
            services.AddScoped<ICountryRepo, DatabaseCountryRepo>();
            services.AddScoped<ILanguageRepo, DatabaseLanguageRepo>();
            services.AddScoped<IPersonLanguageRepo, DatabasePersonLanguageRepo>();

            //------------------------- CORS -----------------------------------------------------------
            services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy",
                    builder =>
                    {
                        builder.WithOrigins("*")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            //------------------------- Swagger -----------------------------------------------------------
            services.AddSwaggerGen();

            //services.AddControllersWithViews();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Persons API V1");
            });

            app.UseRouting();

            //Cors
            app.UseCors();

            app.UseAuthentication(); // Add this - i.e are you logged in?
            app.UseAuthorization();  // Add this - i.e do you have the right to log in?

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
