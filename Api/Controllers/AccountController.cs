using System.Threading.Tasks;
using Api.Requests;
using Api.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var authResponse = await _userManager.RegisterAsync(request.Email, request.Password);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            var response = new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                ExpiresIn = authResponse.ExpiresIn
            };
            return Ok(new AuthResponse<AuthSuccessResponse>(response));

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var authResponse = await _userManager.LoginAsync(request.Email, request.Password);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            var response = new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                ExpiresIn = authResponse.ExpiresIn
            };
            return Ok(new AuthResponse<AuthSuccessResponse>(response));

        }
    }
}