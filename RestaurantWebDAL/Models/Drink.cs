using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebDAL.Models
{
    public class Drink : BaseEntity
    {
        [MaxLength(255)]
        [MinLength(3)]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public decimal Volume { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public decimal Price { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}
