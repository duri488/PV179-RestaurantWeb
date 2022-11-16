namespace RestaurantWebBL.DTOs;

public class DailyMenuUpdateDto : BaseEntityDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public decimal MenuPrice { get; set; }
}