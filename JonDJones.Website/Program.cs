using JonDJonesUmbraco9SampleSite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

var builder = Host.CreateDefaultBuilder()
    .ConfigureUmbracoDefaults()
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStaticWebAssets();
        webBuilder.UseStartup<Startup>();
    })
    .ConfigureLogging(x => x.ClearProviders())
    .ConfigureAppConfiguration((ctx, builder) =>
    {
        var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        builder.AddJsonFile("appsettings.json", false, true);
        builder.AddJsonFile($"appsettings.{enviroment}.json", true, true);
        builder.AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true);

        builder.AddEnvironmentVariables();
    });


var host = builder.Build();
host.Run();

