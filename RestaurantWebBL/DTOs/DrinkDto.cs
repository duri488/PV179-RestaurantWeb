namespace RestaurantWebBL.DTOs;

public class DrinkDto : BaseEntityDto
{
    public string Name { get; set; }
    public decimal Volume { get; set; }
    public decimal Price { get; set; }
    public List<RestaurantDto> Restaurants { get; set; }
}