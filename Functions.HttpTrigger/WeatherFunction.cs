using System.IO;
using System.Net;
using System.Threading.Tasks;
using Functions.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Functions.HttpTrigger;

public class WeatherFunction
{
    private readonly ILogger<WeatherFunction> _logger;
    private readonly IWeatherService _service;

    public WeatherFunction(
        ILogger<WeatherFunction> logger,
        IWeatherService service)
    {
        _logger = logger;
        _service = service;
    }

    [FunctionName("WeatherFunction")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("Weather forecast function service");

        var forecast = await _service.WeatherForecast("London");
        return new OkObjectResult(forecast);
    }
}

