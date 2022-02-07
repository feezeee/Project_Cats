using AutoMapper;
using BLL.Services;
using Cat.API.Response;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cat.API.Request;

namespace Cat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IAccountService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(ILogger<AuthorizationController> logger, IAccountService userService, IMapper mapper, IConfiguration configuration, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
            _authorizationService = authorizationService;
        }

        [HttpPost("Login")]        
        public async Task<IActionResult> Authorizate([FromBody] PostAccountRequest postAccountRequest)
        {
            string login = postAccountRequest.Login;
            string password = postAccountRequest.Password;
            var authorization = await _authorizationService.Authenticate(login, password);
            if (authorization == null)
            {
                return BadRequest("Неверный логин и(или) пароль!");
            }
            var authorizateionResponse = _mapper.Map<AuthorizationResponse>(authorization);
            return Ok(authorizateionResponse);
        }

        
    }
}
