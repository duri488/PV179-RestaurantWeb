namespace PV179_RestaurantWeb.Models;

public class WeeklyMenuViewModel
{
    public int? Id { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public List<DailyMenuViewModel> DailyMenusEnumerable { get; set; } = new ();
}