namespace RestaurantWebBL.DTOs;

public class DrinkDto : BaseEntityDto
{
    public string Name { get; set; }
    public decimal Volume { get; set; }
    public decimal Price { get; set; }
    public int AllergenFlags { get; set; }
    public RestaurantDto Restaurant { get; set; }
}