using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.QueryObjects;

public class DailyMenuQueryObject : IDailyMenuQueryObject
{
    private readonly IQueryFactory<DailyMenu> _queryFactory;
    private readonly IMapper _mapper;

    public DailyMenuQueryObject(IMapper mapper, IQueryFactory<DailyMenu> queryFactory)
    {
        _mapper = mapper;
        _queryFactory = queryFactory;
    }

    public QueryResultDto<DailyMenuDto> DailyMenusAssociatedToWeeklyMenu(DailyMenuFilterDto filter)
    {
        if (!filter.WeeklyMenuId.HasValue) throw new ArgumentNullException(nameof(filter.WeeklyMenuId));
        IQuery<DailyMenu> query = _queryFactory.Build()
            .Where<int>(a => a == filter.WeeklyMenuId.Value , nameof(DailyMenu.WeeklyMenuId));

        if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
        {
            query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
        }
        if (filter.RequestedPageNumber.HasValue)
        {
            query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
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