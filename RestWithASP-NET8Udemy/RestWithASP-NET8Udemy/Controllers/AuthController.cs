using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASP_NET8Udemy.Business;
using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
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
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var token = _loginBusiness.ValidateCredentials(user);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }


        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null)
            {
                return BadRequest("Invalid client request");
            }

            var token = _loginBusiness.ValidateCredentials(tokenVo);

            if (token == null)
            {
                return BadRequest("Invalid client request");
            }
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(userName);

            if (!result)
            {
                return BadRequest("Invalid client request");
            }
            return NoContent();
        }
    }
}
