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
        private readonly IEncryption encryption;
        public AuthorizationService(IUnitOfWork unitOfWork, IAccountService accountService, IAccountFinder accountFinder, IConfiguration configuration, IEncryption encryption)
        {
            this.accountFinder = accountFinder;
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            this.accountService = accountService;
            this.encryption = encryption;
        }

        public async Task<(string jwtToken, string refreshToken)> Authenticate(Account account, string ipAddress)
        {        
            var jwtToken = GenerateJwtToken(account);
            var refreshToken = GenerateRefreshToken(ipAddress);   
            var result = (jwtToken: jwtToken, refreshToken: refreshToken.Token);
            refreshToken.Token = encryption.Encrypt(refreshToken.Token);
            account.RefreshTokens.Add(refreshToken);
            await unitOfWork.Save();
            return result;
        }
        public async Task<(string jwtToken, string refreshToken)> RefreshToken(Account account, RefreshToken oldRefreshToken , string ipAddress)
        {
            
            var jwtToken = GenerateJwtToken(account);
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            var result = (jwtToken: jwtToken, refreshToken: newRefreshToken.Token);
            newRefreshToken.Token = encryption.Encrypt(newRefreshToken.Token);

            oldRefreshToken.Revoked = newRefreshToken.Created;
            oldRefreshToken.RevokedByIp = newRefreshToken.CreatedByIp;
            oldRefreshToken.ReplacedByToken = newRefreshToken.Token;

            account.RefreshTokens.Add(newRefreshToken);
            await unitOfWork.Save();
            return result;
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
