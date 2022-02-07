using BLL.Entities;
using BLL.Finders;
using BLL.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        IAccountFinder accountFinder { get; set; }
        IConfiguration configuration { get; set; }
        public AuthorizationService(IAccountFinder accountFinder, IConfiguration configuration)
        {
            this.accountFinder = accountFinder;
            this.configuration = configuration;
        }
        public async Task<Authorization> Authenticate(string login, string password)
        {
            var user = await accountFinder.GetByLoginAndPassword(login, password);
            if (user == null)
            {
                return null;
            }
            var identity = GetIdentity(user);

            var now = DateTime.UtcNow;
            double lifetime;
            double.TryParse(configuration["Jwt:LIFETIME"], out lifetime);
            var jwt = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    expires: now.Add(TimeSpan.FromMinutes(lifetime)),
                    claims: identity.Claims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new Authorization { Login = login, Role = user.Role, Token = encodedJwt };
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
