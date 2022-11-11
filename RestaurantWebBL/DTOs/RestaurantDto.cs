namespace RestaurantWebBL.DTOs;

public class RestaurantDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longtitude { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public List<MealDto> Meals { get; set; } = new ();
    public List<DrinkDto> Drinks { get; set; } = new();
    public List<WeeklyMenuDto> WeeklyMenus { get; set; } = new();
}