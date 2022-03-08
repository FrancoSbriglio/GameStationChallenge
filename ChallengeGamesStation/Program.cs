using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ChallengeGamesStation;
using Microsoft.AspNetCore;
using Serilog;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
            true)
        .AddEnvironmentVariables()
        .Build();


    public static int Main(string[] args)
    {
        var name = Assembly.GetExecutingAssembly().GetName();
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Assembly", $"{name.Name}")
            .Enrich.WithProperty("Version", $"{name.Version}")
            .CreateLogger();


        try
        {
            Log.Information("Getting the motors running...");

            CreateWebHostBuilder(args).Build().Run();

            return 0;
        }
        catch (System.Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

 
    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

    }
}