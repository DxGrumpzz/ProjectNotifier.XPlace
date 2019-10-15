namespace ProjectNotifier.XPlace.WebServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;

    public class Startup
    {

        private readonly IConfiguration _confg;

        private readonly IProjectLoader _projectLoader;


        public Startup(IConfiguration confg)
        {
            _confg = confg;
            _projectLoader = new ProjectLoader(confg["ProjectLoderUrl"]);
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add database to servicess
            services.AddDbContext<AppDBContext>(options =>
            {
                // Use SQL server as the database
                options.UseSqlServer(_confg.GetConnectionString("Default"));
            });


            // Add identity sotres for password hashers, UserManagers, and roles
            services.AddIdentity<AppUserModel, IdentityRole>()
            .AddEntityFrameworkStores<AppDBContext>()
            // Add a provider that generates unique tokens for things like user's profile update requests
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Allow weak passwords, For now
                options.Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequiredLength = 4,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                };
            });

            // Add controllers without views
            services.AddControllers(config =>
            {
                // Disable Endpoint routing for now, maybe I'll use it in the futures
                config.EnableEndpointRouting = false;
            });
            
            services.AddSingleton(new ProjectList(_projectLoader));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider, AppDBContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Identity setup
            app.UseAuthentication();

            // Ensure database was created
            dbContext.Database.EnsureCreated();

            // Not using async await because it will probably cause a race condition.
            // In addition there is no point in async call here because here is where the server is being set-up
            provider.GetService<ProjectList>().UpdateListAsync().Wait();

            app.UseMvc();
        }
    }
}
