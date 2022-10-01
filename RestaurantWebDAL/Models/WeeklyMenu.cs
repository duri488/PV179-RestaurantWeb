using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebDAL.Models
{
    public class WeeklyMenu : BaseEntity
    {
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        public int MealId { get; set; }
        public int RestaurantId { get; set; }
    }
}
