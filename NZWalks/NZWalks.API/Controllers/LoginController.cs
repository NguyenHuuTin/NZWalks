using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Responsitories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserReponsitory userReponsitory;
        private readonly ITokenHandle tokenHandle;

        public LoginController(IUserReponsitory userReponsitory, ITokenHandle tokenHandle)
        {
            this.userReponsitory = userReponsitory;
            this.tokenHandle = tokenHandle;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var user = await userReponsitory.AuthenticationAsync(loginRequest.UserName, loginRequest.Password);
            if (user != null)
            {
                // JWT token
                var token = tokenHandle.CreateTokenAsync(user);
               
                return Ok(token);
            }

            return BadRequest("UserName or PassWord incorrect.");
        }
    }
}
