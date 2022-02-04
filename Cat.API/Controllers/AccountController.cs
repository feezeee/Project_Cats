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
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountController(ILogger<AccountController> logger, IAccountService userService, IMapper mapper, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;

        }

        [HttpPost("token")]        
        public async Task<IActionResult> Token([FromBody] PostAccountRequest postAccountRequest)
        {
            string username = postAccountRequest.Name;
            string password = postAccountRequest.Password; 
            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var now = DateTime.UtcNow;
            double lifetime;
            double.TryParse(_configuration["Jwt:LIFETIME"], out lifetime);
            var jwt = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(lifetime)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var userIdentity = await _userService.GetByNameAndPassword(username, password);
            UserIdentityResponse user = _mapper.Map<UserIdentityResponse>(userIdentity);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
