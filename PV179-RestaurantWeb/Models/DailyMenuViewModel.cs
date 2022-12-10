using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PV179_RestaurantWeb.Models;

public class DailyMenuViewModel
{
    public int Id { get; set; }
    [DisplayName("Day")]
    [EnumDataType(typeof(DayOfWeek))]
    public DayOfWeek DayOfWeek { get; set; }
    [DataType(DataType.Date)]
    public DateTime Date {
        get
        {
            return Enumerable.Range(0, 1 + WeeklyMenu.DateTo.Subtract(WeeklyMenu.DateFrom).Days)
                .Select(offset => WeeklyMenu.DateFrom.AddDays(offset))
                .First(d => d.DayOfWeek == DayOfWeek); 
        } }
    [DataType(DataType.Currency)]
    [DisplayName("Price")]
    public decimal MenuPrice { get; set; }
    public MealViewModel Meal { get; set; }
    [DisplayName("Week")]
    public WeeklyMenuViewModel WeeklyMenu { get; set; }
}