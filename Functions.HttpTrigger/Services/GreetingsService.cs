using Functions.Contracts;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Functions.HttpTrigger.Services;

public class GreetingsService : IGreetingsService
{
    private readonly ILogger<GreetingsService> _logger;

    public GreetingsService(ILogger<GreetingsService> logger)
    {
        _logger = logger;
    }

    public async Task<string> GetGreeting(string name)
    {
        _logger.LogInformation("In GetGreetings service method: {name}", name);
        return await Task.FromResult($"Hello {name}");
    }
}
