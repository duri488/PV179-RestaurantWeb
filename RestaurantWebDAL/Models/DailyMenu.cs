using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWebDAL.Models
{
    public class DailyMenu : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal MenuPrice { get; set; }
        public int? WeeklyMenuId { get; set; }
        public WeeklyMenu WeeklyMenu { get; set; }
        public int? MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
