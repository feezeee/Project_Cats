using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Finders
{
    internal interface IRefreshTokenFinder
    {
        Task<List<RefreshToken>> Get();

        Task<RefreshToken> GetById(int id);

        Task<List<RefreshToken>> GetByToken(string token);
    }
}
