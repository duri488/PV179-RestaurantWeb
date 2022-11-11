using Optional;

namespace RestaurantWebDomain;

public class DailyMenu
{
    public DayOfWeek DayOfWeek { get; set; }
    public decimal MenuPrice { get; set; }
    public Option<int> WeeklyMenuId { get; set; }
    public Option<int> MealId { get; set; }
    public Meal Meal { get; set; }
}