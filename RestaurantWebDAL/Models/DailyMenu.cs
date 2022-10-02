using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebDAL.Models
{
    public class DailyMenu : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public decimal MenuPrice { get; set; }
        public int? WeeklyMenuId { get; set; }
        public WeeklyMenu WeeklyMenu { get; set; }
        public int? MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
