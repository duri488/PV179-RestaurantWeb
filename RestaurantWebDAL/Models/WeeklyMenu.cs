using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantWeb.Contract;

namespace RestaurantWebDAL.Models
{
    public class WeeklyMenu : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime DateFrom { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime DateTo { get; set; }
        public int? MealId { get; set; }
        public Meal Meal { get; set; }
        public int? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
