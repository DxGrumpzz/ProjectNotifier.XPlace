namespace ProjectNotifier.XPlace.WebServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ProjectNotifier.XPlace.Core;
    using System;

    public class Startup
    {

        private readonly IConfiguration _confg;

        public Startup(IConfiguration confg)
        {
            _confg = confg;
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
            // Add Hebrew identityErrors to error describer
            .AddErrorDescriber<HebrewIdentityErrorDescriber>()
            // Add AppDBContext as the identity provider
            .AddEntityFrameworkStores<AppDBContext>()
            // Add a provider that generates unique tokens for things like user's profile update requests
            .AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            });


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

            // Adds ProjectTypeProcessor as a transient instance
            services.AddTransient<IProjectTypeProcessor, ProjectTypeProcessor>();

            // Add the project loader
            services.AddScoped<IProjectLoader, ProjectLoader>((provider) =>
                new ProjectLoader( 
                    projectTypeProcessor: provider.GetService<IProjectTypeProcessor>(),
                    url: _confg["ProjectLoderUrl"]));


            // Add project list as a singelton
            services.AddSingleton(
                (provider) =>
                new ProjectList(provider.GetService<IProjectLoader>()));


            // Add a timed notifier
            services.AddSingleton<Notifier>();


            // Add signlarR
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider, IHubContext<ProjectsHub> projectsHub, ProjectList projectList, AppDBContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Identity setup
            // Add routing to make SignalR hubs work 
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(config =>
            {
                config.MapHub<ProjectsHub>("/ProjectsHub");
            });


            app.UseMvc();


            // Ensure database was created
            if (dbContext.Database.EnsureCreated())
            {
                // Add roles
                provider.GetService<RoleManager<IdentityRole>>().CreateAsync(new IdentityRole("User")).Wait();
                provider.GetService<RoleManager<IdentityRole>>().CreateAsync(new IdentityRole("Admin")).Wait();
            };


            // Not using async await because it will probably cause a race condition.
            // In addition there is no point in async call here because here is where the server is being set-up
            provider.GetService<ProjectList>().UpdateListAsync().Wait();

            // Add notifications
            provider.GetService<Notifier>().Notifications.Add(new NotificationItem((int)TimeSpan.FromMinutes(15).TotalMilliseconds,
            async () =>
            {
                await projectList.UpdateListAsync();

                await projectsHub.Clients.All.SendAsync("ProjectListUpdated", projectList.Projects);

            }, "ProjectLoader"));
        }
    };
};