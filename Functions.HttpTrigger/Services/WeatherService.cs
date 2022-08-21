using System;
using System.Net.Http;
using Functions.Contracts;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Functions.HttpTrigger.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WeatherService(IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient?.CreateClient("WeatherApi");
        _configuration = configuration 
            ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<string> WeatherForecast(string city)
    {
        var apiKey = _configuration.GetValue<string>("ApiKey");

        var result = await _httpClient.GetAsync($"?key={apiKey}&q={city}&aqi=no");
        return await result.Content.ReadAsStringAsync(); ;
    }
}
