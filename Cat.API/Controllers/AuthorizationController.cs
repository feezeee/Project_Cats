using AutoMapper;
using BLL.Services;
using Cat.API.Response;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cat.API.Request;
using Microsoft.AspNetCore.Authorization;
using BLL.Entities;

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
        private readonly BLL.Services.IAuthorizationService _authorizationService;

        public AuthorizationController(ILogger<AuthorizationController> logger, IAccountService userService, IMapper mapper, IConfiguration configuration, BLL.Services.IAuthorizationService authorizationService)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
            _authorizationService = authorizationService;
        }

        [HttpPost("Login")]   
        [AllowAnonymous]
        public async Task<IActionResult> Authorizate([FromBody] PostAuthenticateRequest postAuthenticateRequest)
        {
            try
            {
                var requestAuthenticate = _mapper.Map<Authentication>(postAuthenticateRequest);

                if (Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    requestAuthenticate.IpAddress = Request.Headers["X-Forwarded-For"];
                }
                else
                {
                    requestAuthenticate.IpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                }

                var authorization = await _authorizationService.Authenticate(requestAuthenticate);
                if (authorization == null)
                {
                    return BadRequest("Неверный логин и(или) пароль!");
                }
                var authorizateionResponse = _mapper.Map<AuthorizationResponse>(authorization);
                return Ok(authorizateionResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        
    }
}
