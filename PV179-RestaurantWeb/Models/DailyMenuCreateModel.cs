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
    public IEnumerable<MealDto>? Meal { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
    [Required(ErrorMessage = "This field is required")]
    public int? MealId { get; set; }
    [DisplayName("Week")]
    public IEnumerable<WeeklyMenuDto>? WeeklyMenu { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
    [Required(ErrorMessage = "This field is required")]
    public int? WeeklyMenuId { get; set; }
}