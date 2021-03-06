using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Account
    {
        public string Login { get; set; }        
        public string Password { get; set; }        
        public string Role { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
