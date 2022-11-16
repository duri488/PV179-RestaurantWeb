namespace RestaurantWebBL.DTOs;

public class WeeklyMenuUpdateDto : BaseEntityDto
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}