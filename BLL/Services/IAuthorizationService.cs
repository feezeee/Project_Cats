using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAuthorizationService
    {
        Task<(string jwtToken, string refreshToken)> Authenticate(Account account, string ipAddress);
        Task<(string jwtToken, string refreshToken)> RefreshToken(Account account, RefreshToken oldRefreshToken, string ipAddress);
    }
}
