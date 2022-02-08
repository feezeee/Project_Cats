using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    [Owned]
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires {get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;
        public DateTime? Revoked    {get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        
    }
}
