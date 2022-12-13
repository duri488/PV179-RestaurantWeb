using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RestaurantWebBL.DTOs;

namespace PV179_RestaurantWeb.Models;

public class DailyMenuCreateModel
{
    [DisplayName("Day")]
    [EnumDataType(typeof(DayOfWeek))]
    [Required(ErrorMessage = "This field is required")]
    public DayOfWeek? DayOfWeek { get; set; }
    [DataType(DataType.Currency)]
    [DisplayName("Price")]
    public decimal MenuPrice { get; set; }
    [Required(ErrorMessage = "This field is required")]
    public IEnumerable<MealDto> Meal { get; set; }
    public int MealId { get; set; }
    [DisplayName("Week")]
    [Required(ErrorMessage = "This field is required")]
    public IEnumerable<WeeklyMenuDto> WeeklyMenu { get; set; }
    public int WeeklyMenuId { get; set; }
}