using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiVersion("1")]
[Route("api/v{verion:apiVersion}/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
}
