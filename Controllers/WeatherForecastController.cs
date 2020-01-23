using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using error_tester.Exceptions;

namespace error_tester.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Handled")]
        public IActionResult Handled() {
            try {
                var foo = 0;
                var c = 2 / foo;
            }catch(Exception ex) {
                //NewRelic.Api.Agent.NewRelic.NoticeError(ex);
                return StatusCode(400);
            }

            return Ok(new {status = "True"});
        }

        [HttpGet("HandledWithWrappedError")]
        public IActionResult HandledWithWrappedError() {
            try {
                try {
                    var foo = 0;
                    var c = 2 / foo;
                }catch(Exception ex) {
                    throw new ErrorTesterExceptions("Woah, dividing by zero", ex);
                }
            } catch (Exception ex2) {
                NewRelic.Api.Agent.NewRelic.NoticeError(ex2);
                return StatusCode(400);
            }
            return Ok(new {status = "True"});
        }

        [HttpGet("HandledWithError")]
        public IActionResult HandledWithError() {
            try {
                var foo = 0;
                var c = 2 / foo;
            }catch(Exception ex) {
                NewRelic.Api.Agent.NewRelic.NoticeError(ex);
                return StatusCode(400);
            }

            return Ok(new {status = "True"});
        }

        [HttpGet("Unhandled")]
        public IActionResult Unhandled() {
            var foo = 0;
            var c = 2 / foo;
            return Ok(new {status = "True"});
        }
    }
}
