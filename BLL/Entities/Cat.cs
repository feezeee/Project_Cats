using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entities
{
    
    [Table("Cats")]
    public class Cat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
