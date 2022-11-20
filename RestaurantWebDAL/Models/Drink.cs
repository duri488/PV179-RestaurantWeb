using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWebDAL.Models
{
    public class Drink : BaseEntity
    {
        [MaxLength(255)]
        [MinLength(3)]
        public string Name { get; set; }

        public decimal Volume { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int AllergenFlags { get; set; }
        public int? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
