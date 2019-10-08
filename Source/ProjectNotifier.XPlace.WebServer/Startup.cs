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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDBContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Identity setup
            app.UseAuthentication();

            // Ensure database was created
            dbContext.Database.EnsureCreated();


            app.UseMvc();
        }
    }
}
