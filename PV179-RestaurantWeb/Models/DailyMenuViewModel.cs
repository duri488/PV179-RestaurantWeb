using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PV179_RestaurantWeb.Models;

public class DailyMenuViewModel
{
    public int Id { get; set; }
    [DisplayName("Day")]
    public DayOfWeek DayOfWeek { get; set; }
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    [DataType(DataType.Currency)]
    [DisplayName("Price")]
    public decimal MenuPrice { get; set; }
    public MealViewModel Meal { get; set; }
}