namespace ProjectNotifier.XPlace.WebServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers without views
            services.AddControllers(config =>
            {
                // Disable Endpoint routing for now, maybe I'll use it in the futures
                config.EnableEndpointRouting = false;
            })
            // Customize json serializer
            .AddJsonOptions(config =>
            {
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                //config.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
