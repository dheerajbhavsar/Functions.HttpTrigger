using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Functions.HttpTrigger.Models;
using Functions.Contracts;

namespace Functions.HttpTrigger
{
    public class GreeterFunction
    {
        private readonly ILogger<GreeterFunction> _logger;
        private readonly IGreetingsService _service;

        public GreeterFunction(
            ILogger<GreeterFunction> logger, 
            IGreetingsService service)
        {
            _logger = logger;
            _service = service;
        }

        [FunctionName("GreeterFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] Greet greet)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var greets = await _service.GetGreeting(greet.name);

            return new OkObjectResult(greets);
        }
    }
}
