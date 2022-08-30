using Functions.HttpTrigger;
using Functions.HttpTrigger.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using Functions.Contracts;
//using Serilog;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Functions.HttpTrigger;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var config = builder.GetContext().Configuration;
        var baseAddress = config.GetValue<string>("WeatherApiUrl");
        builder.Services.AddHttpClient("WeatherApi", opt =>
        {
            opt.BaseAddress = new Uri(baseAddress);
        });
        builder.Services.AddScoped<IGreetingsService, GreetingsService>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();

        //var logger = new LoggerConfiguration()
        //    .MinimumLevel.Debug()
        //    .WriteTo.Console()
        //    .WriteTo.File(@"log.txt", rollingInterval: RollingInterval.Day)
        //    .CreateLogger();

        //builder.Services.AddLogging(c => c.AddSerilog(logger));
    }
 
}
