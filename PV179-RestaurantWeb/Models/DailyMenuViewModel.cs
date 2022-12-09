using System.ComponentModel.DataAnnotations;

namespace PV179_RestaurantWeb.Models;

public class DailyMenuViewModel
{
    public int Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    public decimal MenuPrice { get; set; }
    public MealViewModel Meal { get; set; }
}