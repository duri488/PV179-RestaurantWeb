using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebDAL.Models
{
    public class WeeklyMenu : BaseEntity
    {
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
