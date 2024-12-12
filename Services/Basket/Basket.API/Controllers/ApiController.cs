using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[Route("api/v{verion:apiVersion}/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
}
