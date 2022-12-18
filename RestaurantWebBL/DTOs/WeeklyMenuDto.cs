namespace RestaurantWebBL.DTOs;

public class WeeklyMenuDto : BaseEntityDto
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}