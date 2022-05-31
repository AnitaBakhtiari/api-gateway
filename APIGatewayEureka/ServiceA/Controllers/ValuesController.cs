using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers;

[Route("api/[controller]")]
public class ValuesController : Controller
{
    private static int _count;

    // GET api/values
    [HttpGet]
    [Route("test")]
    public string GetSingle()
    {
        _count++;

        Console.WriteLine($"get...{_count}");

        if (_count <= 3) Thread.Sleep(5000);

        return $"I'm Service A -- {Request.Host.Port} -- you are testing timeout and circuit breaker";
    }

    [HttpGet]
    public string Get()
    {
        return $"I'm Service A -- {Request.Host.Port}";
    }

    [HttpGet]
    [HttpHead]
    [Route("healthcheck")]
    public IActionResult HealthCheck()
    {
        return Ok();
    }

    [HttpGet("info")]
    public string Info()
    {
        return "ServiceA - info";
    }
}