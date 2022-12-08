namespace RestaurantWebBL.DTOs;

public class RestaurantDto : BaseEntityDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}