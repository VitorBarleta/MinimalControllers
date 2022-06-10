using Microsoft.AspNetCore.Mvc;
using TraditionalControllers.TestHarness.Models;

namespace TraditionalControllers.TestHarness.Controllers;

[ApiController]
[Route("customer")]
public class WeatherForecastController : ControllerBase
{
    class CreateCustomerRequest
    {
        public string Name { get; set; } = null!;
    }

    [HttpGet]
    public Task<IActionResult> CreateUser()
    {
        return Task.FromResult(Ok(new Customer() { Name = "Robson" }) as IActionResult);
    }
}
