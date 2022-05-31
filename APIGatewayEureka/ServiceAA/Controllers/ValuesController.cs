using Microsoft.AspNetCore.Mvc;

namespace ServiceAA.Controllers;

[Route("api/[controller]")]
public class ValuesController : Controller
{
    // api/values
    [HttpGet]
    public string Get()
    {
        return $"I'm Service AA  -- {Request.Host.Port}";
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
        return "Service AA - info";
    }
}