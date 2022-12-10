namespace RestaurantWebBL.DTOs.FilterDTOs;

public class WeeklyMenuFilterDto
{
    public DateTime Date { get; set; }
    public int? RequestedPageNumber { get; set; }
    public int PageSize { get; set; } = 10;
    public string SortCriteria { get; set; } = "";
    public bool SortAscending { get; set; }
}