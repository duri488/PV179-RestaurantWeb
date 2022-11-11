namespace RestaurantWebDomain;

public class Meal
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
    public List<Restaurant> Restaurants { get; set; }
}