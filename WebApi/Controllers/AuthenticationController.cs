namespace CompanyName.Application.WebApi.OrdersApi.Controllers
{
    using AutoMapper;
    using CompanyName.Application.Services.AuthService.Models;
    using CompanyName.Application.Services.AuthService.Services;
    using CompanyName.Application.WebApi.OrdersApi.Models.Auth.Requests;
    using CompanyName.Application.WebApi.OrdersApi.Models.Auth.Responses;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService service;

        private readonly IMapper mapper;

        public AuthenticationController(
            IAuthService authService,
            IMapper autoMapper)
        {
            service = authService;
            mapper = autoMapper;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userRegister = mapper.Map<UserRegisterRequest, UserRegister>(request);
            var authResult = await service.RegisterUser(userRegister);

            var response = mapper.Map<AuthResult, AuthResponse>(authResult);

            if (response.Success)
            { 
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userRegister = mapper.Map<UserLoginRequest, UserLogin>(request);
            var loginResult = await service.LoginUser(userRegister);

            var response = mapper.Map<AuthResult, AuthResponse>(loginResult);
            
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
