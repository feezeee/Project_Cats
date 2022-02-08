using BLL.Entities;
using BLL.Finders;
using BLL.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAccountFinder accountFinder;
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAccountService accountService;
        public AuthorizationService(IUnitOfWork unitOfWork, IAccountService accountService, IAccountFinder accountFinder, IConfiguration configuration)
        {
            this.accountFinder = accountFinder;
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            this.accountService = accountService;
        }
        public async Task<Authorization> Authenticate(Authentication auntefication)
        {
            var user = await accountFinder.GetByLoginAndPassword(auntefication.Login, auntefication.Password);
            if (user == null)
            {
                return null;
            }
            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(auntefication.IpAddress);

            user.RefreshTokens.Add(refreshToken);
            accountService.Update(user);
                        
            return new Authorization { Login = user.Login, Role = user.Role, JwtToken = jwtToken, RefreshToken = refreshToken.Token};
        }

        private string GenerateJwtToken(Account account)
        {
            var identity = GetIdentity(account);
            var now = DateTime.UtcNow;
            double lifetime;
            double.TryParse(configuration["Jwt:LIFETIME"], out lifetime);
            var jwt = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    expires: now.Add(TimeSpan.FromMinutes(lifetime)),
                    claims: identity.Claims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                var refreshToken = Convert.ToBase64String(randomNumber);

                int LifeTimeDays;
                int.TryParse(configuration["Refreshtoken:LifeTimeDays"], out LifeTimeDays);
                return new RefreshToken
                {
                    Token = refreshToken,
                    Expires = DateTime.UtcNow.AddDays(LifeTimeDays),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }            
        }

        private ClaimsIdentity GetIdentity(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role)
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;  
        }
    }
}
