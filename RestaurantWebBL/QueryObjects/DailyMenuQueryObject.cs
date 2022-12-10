using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.QueryObjects;

public class DailyMenuQueryObject : IDailyMenuQueryObject
{
    private readonly IQuery<DailyMenu> _query;
    private readonly IMapper _mapper;

    public DailyMenuQueryObject(IMapper mapper, IQuery<DailyMenu> query)
    {
        _mapper = mapper;
        _query = query;
    }

    public QueryResultDto<DailyMenuDto> DailyMenusAssociatedToWeeklyMenu(DailyMenuFilterDto filter)
    {
        if (!filter.WeeklyMenuId.HasValue) throw new ArgumentNullException(nameof(filter.WeeklyMenuId));
        IQuery<DailyMenu> query = _query
            .Where<int>(a => a == filter.WeeklyMenuId.Value , nameof(DailyMenu.WeeklyMenuId));

        if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
        {
            query = query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
        }
        if (filter.RequestedPageNumber.HasValue)
        {
            query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
        }

        IEnumerable<DailyMenu> items = query.Execute();
        IEnumerable<DailyMenuDto> dailyMenuDtos = _mapper.Map<IEnumerable<DailyMenuDto>>(items).ToList();
     
        return new QueryResultDto<DailyMenuDto>
        {
            Items = dailyMenuDtos,
            PageSize = filter.PageSize,
            RequestedPageNumber = filter.RequestedPageNumber,
            TotalItemsCount = dailyMenuDtos.Count()
        };
    }
}