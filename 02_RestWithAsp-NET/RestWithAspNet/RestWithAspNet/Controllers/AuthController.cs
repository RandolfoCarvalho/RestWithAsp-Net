using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet.Business;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;

namespace RestWithAspNet.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }
        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Invalid Client request");
            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null) return Unauthorized();
            return Ok(token);

        }
        [HttpPost]
        [Route("refresh")]
        public IActionResult refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Invalid Client request");
            var token = _loginBusiness.ValidateCredentials(tokenVO);
            if (token == null) return BadRequest("Token is null");
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(userName);
            if (!result) return BadRequest("Invalid Client Request");
            return NoContent();
        }
    }
}

