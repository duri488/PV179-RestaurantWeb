using System.ComponentModel.DataAnnotations;

namespace PV179_RestaurantWeb.Models
{
    public class MealUpdateModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        [Required]
        public string Allergens { get; set; }
    }
}
