using System.Threading.Tasks;

namespace Functions.Contracts;

public interface IWeatherService
{
    Task<string> WeatherForecast(string city);
}
