using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantWeb.Contract;

namespace RestaurantWebDAL.Models
{
    public class Drink : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        [MinLength(3)]
        public string Name { get; set; }

        public decimal Volume { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
