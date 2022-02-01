using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    
    [Table("Cats")]
    public class CatDAL
    {
        [Column("CatId")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Column("Price")]
        public double Price { get; set; }

    }
}
