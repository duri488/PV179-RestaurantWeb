using RestaurantWebBL.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PV179_RestaurantWeb.Models
{
    public class DrinkCreateModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Volume { get; set; }
        [Required]
        public decimal Price { get; set; }
        public IEnumerable<AllergenViewModel> Allergens { get; set; }

        public DrinkDto drinkDto() => new()
        {
            Id = Id,
            Name = Name,
            Volume = Volume,
            Price = Price,
        };
    }
}
