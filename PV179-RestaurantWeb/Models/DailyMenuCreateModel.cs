using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PV179_RestaurantWeb.Models;

public class DailyMenuCreateModel
{
    [DisplayName("Day")]
    [EnumDataType(typeof(DayOfWeek))]
    public DayOfWeek DayOfWeek { get; set; }
    [DataType(DataType.Currency)]
    [DisplayName("Price")]
    public decimal MenuPrice { get; set; }
    public int Meal { get; set; }
    [DisplayName("Week")]
    public int WeeklyMenu { get; set; }
}