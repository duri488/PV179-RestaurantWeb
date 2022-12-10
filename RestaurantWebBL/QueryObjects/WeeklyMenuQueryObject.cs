using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.QueryObjects;

public class WeeklyMenuQueryObject : IWeeklyMenuQueryObject
{
    private readonly IQuery<WeeklyMenu> _query;
    private readonly IMapper _mapper;

    public WeeklyMenuQueryObject(IQuery<WeeklyMenu> query, IMapper mapper)
    {
        _query = query;
        _mapper = mapper;
    }

    public QueryResultDto<WeeklyMenuDto> WeeklyMenuByDate(WeeklyMenuFilterDto filter)
    {
        IQuery<WeeklyMenu> query = _query
            .Where<DateTime>(a => a >= filter.Date, nameof(WeeklyMenu.DateFrom))
            .Where<DateTime>(a => a <= filter.Date, nameof(WeeklyMenu.DateTo));

        if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
        {
            query = query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
        }
        if (filter.RequestedPageNumber.HasValue)
        {
            query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
        }

        IEnumerable<WeeklyMenu> items = query.Execute();
        IEnumerable<WeeklyMenuDto> weeklyMenuDtos = _mapper.Map<IEnumerable<WeeklyMenuDto>>(items).ToList();
     
        return new QueryResultDto<WeeklyMenuDto>
        {
            Items = weeklyMenuDtos,
            PageSize = filter.PageSize,
            RequestedPageNumber = filter.RequestedPageNumber,
            TotalItemsCount = weeklyMenuDtos.Count()
        };
    }
}