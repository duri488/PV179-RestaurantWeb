namespace RestaurantWebBL.DTOs;

public class MealDto : BaseEntityDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
    public RestaurantDto Restaurant { get; set; }
}