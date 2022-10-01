using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebDAL.Models
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Meal> Meals { get; set; } = new ();
        public List<Drink> Drinks { get; set; } = new();
        public List<WeeklyMenu> WeeklyMenus { get; set; } = new();
    }
}
