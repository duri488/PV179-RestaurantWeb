using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantWeb.Contract;

namespace RestaurantWebDAL.Models
{
    public class Meal : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        [MinLength(2)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int AllergenFlags { get; set; } = 0;

        [MaxLength(1000)]
        public string Picture { get; set; }
        public int? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
