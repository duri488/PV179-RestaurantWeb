namespace RestaurantWebBL.DTOs;

public class DailyMenuDto : BaseEntityDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public decimal MenuPrice { get; set; }
    public WeeklyMenuDto WeeklyMenu { get; set; }
    public MealDto Meal { get; set; }
}