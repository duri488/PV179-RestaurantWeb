namespace PV179_RestaurantWeb.Models;

public class MealViewModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
    public IEnumerable<AllergenViewModel> Allergens { get; set; }
}