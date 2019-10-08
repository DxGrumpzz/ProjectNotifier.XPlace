namespace ProjectNotifier.XPlace.WebServer
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
            // Use kestrel Web hostign instead of ISS
            .UseKestrel()
            // Add a logger 
            .ConfigureLogging(logger =>
            {
                // Add a console logger
                logger.AddConsole();
            })
            // Add json configuration
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.json");
            })
            // Specify root folder
            .UseContentRoot(Environment.CurrentDirectory)
            // Use the Startup class as the startup for the service provider and app builder
            .UseStartup<Startup>()
            // Build WebHost
            .Build()
            // Start the webiste
            .Run();
        }
    }
}
