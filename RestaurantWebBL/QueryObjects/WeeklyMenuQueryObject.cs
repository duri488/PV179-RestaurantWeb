using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.QueryObjects;

public class WeeklyMenuQueryObject : IWeeklyMenuQueryObject
{
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;

    public WeeklyMenuQueryObject(IMapper mapper, IServiceProvider serviceProvider)
    {
        _mapper = mapper;
        _serviceProvider = serviceProvider;
    }

    public QueryResultDto<WeeklyMenuDto> WeeklyMenuByDate(WeeklyMenuFilterDto filter)
    {
        var query = _serviceProvider.GetRequiredService<IQuery<WeeklyMenu>>();
            
        query
            .Where<DateTime>(a => a >= filter.Date, nameof(WeeklyMenu.DateTo));

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