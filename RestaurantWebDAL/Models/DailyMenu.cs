using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebDAL.Models
{
    public class DailyMenu : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }
        public decimal MenuPrice { get; set; }
        public int WeeklyMenuId { get; set; }
        public int MealId { get; set; }
    }
}
