//using Egolance.Application.DTOs.Auth;
//using Egolance.Application.Services;
//using Microsoft.AspNetCore.Mvc;
//using LoginRequest = Egolance.Application.DTOs.Auth.LoginRequest;
//using RegisterRequest = Egolance.Application.DTOs.Auth.RegisterRequest;

using Egolance.Application.DTOs.Auth;
using Egolance.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Egolance.Api.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register(RegisterRequest request)
        //{
        //    var token = await _authService.RegisterAsync(request);
        //    return Ok(new { token });
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var token = await _authService.RegisterAsync(request);
            return Ok(new { token });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.LoginAsync(request);
            return Ok(new { token });
        }
    }


}
