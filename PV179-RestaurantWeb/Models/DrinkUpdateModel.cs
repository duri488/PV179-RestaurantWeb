using System.ComponentModel.DataAnnotations;

namespace PV179_RestaurantWeb.Models
{
    public class DrinkUpdateModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Volume { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        //[Required]
        //public int Allergens { get; set; }

    }
}
