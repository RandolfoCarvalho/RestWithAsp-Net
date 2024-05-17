using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestWithAspNet.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    }
}
