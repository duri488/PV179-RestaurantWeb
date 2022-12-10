using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantWebBL.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PV179_RestaurantWeb.Models
{
    public class DrinkCreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Volume { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public IEnumerable<AllergenViewModel> Allergens { get; set; }

    }
}
