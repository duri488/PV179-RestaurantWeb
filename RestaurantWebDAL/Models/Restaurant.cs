using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebDAL.Models
{
    public class Restaurant : BaseEntity
    {
        [MaxLength(255)]
        [MinLength(2)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        [MaxLength(255)]
        public string Phone { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }

        public List<Meal> Meals { get; set; }
        public List<Drink> Drinks { get; set; }
        public List<WeeklyMenu> WeeklyMenus { get; set; }
    }
}
