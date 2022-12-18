using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PV179_RestaurantWeb.Models;

public class MealCreateModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    public string Description { get; set; }
    //public IFormFile Picture { get; set; }
    public string Picture { get; set; }
    [Required]
    public int Allergens { get; set; }
}