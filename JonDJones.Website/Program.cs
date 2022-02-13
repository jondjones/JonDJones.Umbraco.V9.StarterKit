using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace JonDJonesUmbraco9SampleSite
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(x => x.ClearProviders())
                .ConfigureAppConfiguration((ctx, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", false, true);

                    var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    Console.WriteLine("HostingEnvironmentName: '{0}'", enviroment);

                    builder.AddJsonFile($"appsettings.{enviroment}.json", true, true);

                    // Add local developer level configuraion
                    builder.AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true);

                    builder.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}