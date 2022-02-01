using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CatDTO
    {       
        public int Id { get; set; }       
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Price { get; set; }
    }
}
