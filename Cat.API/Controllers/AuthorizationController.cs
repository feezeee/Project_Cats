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
        private readonly IEncryption _encryption;

        public AuthorizationController(ILogger<AuthorizationController> logger, IAccountService userService, IMapper mapper, IConfiguration configuration, BLL.Services.IAuthorizationService authorizationService, IEncryption encryption)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
            _authorizationService = authorizationService;
            _encryption = encryption;
        }

        [HttpPost("Login")]   
        [AllowAnonymous]
        public async Task<IActionResult> Authorizate([FromBody] PostAuthenticateRequest postAuthenticateRequest)
        {
            try
            {
                var login = postAuthenticateRequest.Login;
                var password = postAuthenticateRequest.Password;

                var user = await _userService.GetByLoginAndPassword(login, password);
                if (user == null)
                {
                    return BadRequest("Неверный логин и(или) пароль!");
                }
               
                string ipAddress = GetIpAddress();               

                var authorization = await _authorizationService.Authenticate(user, ipAddress);


                if (authorization.jwtToken == null && authorization.refreshToken == null)
                {
                    return BadRequest("Неверный логин и(или) пароль!");
                }
                var response = new
                {
                    jwtToken = authorization.jwtToken,
                    refreshToken = authorization.refreshToken
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] PostRefreshTokenRequest postRefreshTokenRequest)
        {
            try
            {                
                var user = await _userService.GetByRefreshToken(postRefreshTokenRequest.RefreshToken);
                if (user == null)
                {
                    return BadRequest("Токен не соответствует ни одному пользователю! ");
                }
                var oldRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.Token == _encryption.Encrypt(postRefreshTokenRequest.RefreshToken));

                if (oldRefreshToken == null)
                {
                    return BadRequest("Нет токена!");
                }

                var refreshTokens = await _authorizationService.RefreshToken(user, oldRefreshToken, GetIpAddress());
                if (refreshTokens.jwtToken == null && refreshTokens.refreshToken == null)
                {
                    return BadRequest("Произошла ошибка");
                }
                var response = new
                {
                    jwtToken = refreshTokens.jwtToken,
                    refreshToken = refreshTokens.refreshToken
                };

                return Json(response);
                //return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private string GetIpAddress()
        {
            string ipAddress;
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = Request.Headers["X-Forwarded-For"];
            }
            else
            {
                ipAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            return ipAddress;
        }

    }
}
