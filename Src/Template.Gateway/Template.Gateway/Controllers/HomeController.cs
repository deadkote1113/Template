using Microsoft.AspNetCore.Mvc;

namespace Template.Gateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public object Index()
    {
        return new { Id = 0 };
    }
}
