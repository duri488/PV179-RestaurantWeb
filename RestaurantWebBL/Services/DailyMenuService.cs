using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Services;

public class DailyMenuService : IDailyMenuService
{
    private readonly IEagerLoadingRepository<DailyMenu> _dailyMenuRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IDailyMenuQueryObject _dailyMenuQueryObject;
    
    public DailyMenuService(IEagerLoadingRepository<DailyMenu> dailyMenuRepository,
        IMapper mapper,
        IUnitOfWorkFactory unitOfWorkFactory, IDailyMenuQueryObject dailyMenuQueryObject)
    {
        _dailyMenuRepository = dailyMenuRepository;
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
        _dailyMenuQueryObject = dailyMenuQueryObject;
    }

    public async Task<int> UpsertAsync(DailyMenuDto dailyMenuDto, int? mealId, int weeklyMenuId)
    {
        DailyMenu? dailyMenu = await _dailyMenuRepository.GetByIdAsync(dailyMenuDto.Id);
        if (dailyMenu is null)
        {
            dailyMenuDto.Id = await CreateAsync(dailyMenuDto, mealId, weeklyMenuId);
            return dailyMenuDto.Id;
        }

        await UpdateAsync(dailyMenuDto, mealId, weeklyMenuId);
        return dailyMenuDto.Id;
    }

    public async Task<int> CreateAsync(DailyMenuDto createdEntity, int? mealId, int weeklyMenuId)
    {
        AssertNavigationalPropertiesAreNull(createdEntity);
        var dailyMenu = _mapper.Map<DailyMenu>(createdEntity);
        dailyMenu.MealId = mealId;
        dailyMenu.WeeklyMenuId = weeklyMenuId;
        await using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
        int id = _dailyMenuRepository.Insert(dailyMenu);
        await unitOfWork.CommitAsync();
        return id;
    }

    public async Task<DailyMenuDto?> GetByIdAsync(int entityId)
    {
        return await GetByIdAsync(entityId, false);
    }

    public async Task<DailyMenuDto?> GetByIdAsync(int entityId, bool includeNavigationProperties)
    {
        DailyMenu? dailyMenu = await _dailyMenuRepository.GetByIdAsync(entityId, includeNavigationProperties);
        return _mapper.Map<DailyMenuDto?>(dailyMenu);
    }

    public async Task UpdateAsync(DailyMenuDto updatedEntity, int? mealId, int weeklyMenuId)
    {
        AssertNavigationalPropertiesAreNull(updatedEntity);
        DailyMenu dailyMenu = await _dailyMenuRepository.GetByIdAsync(updatedEntity.Id) ?? 
                              throw new InvalidOperationException($"Entity with id {updatedEntity.Id} does not exist!");
        dailyMenu.MenuPrice = updatedEntity.MenuPrice;
        dailyMenu.DayOfWeek = updatedEntity.DayOfWeek;
        dailyMenu.MealId = mealId;
        dailyMenu.WeeklyMenuId = weeklyMenuId;
        await using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
        _dailyMenuRepository.Update(dailyMenu);
        await unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int entityId)
    {
        await using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
        await _dailyMenuRepository.DeleteAsync(entityId);
        await unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<DailyMenuDto>> GetAllAsync()
    {
        return await this.GetAllAsync(false);
    }

    public async Task<IEnumerable<DailyMenuDto>> GetAllAsync(bool includeNavigationProperties)
    {
        IEnumerable<DailyMenu> dailyMenuList = await _dailyMenuRepository.GetAllAsync(includeNavigationProperties);
        return _mapper.Map<IEnumerable<DailyMenuDto>>(dailyMenuList);
    }
    
    private static void AssertNavigationalPropertiesAreNull(DailyMenuDto dailyMenuDto)
    {
        string operation = dailyMenuDto.Id == 0 ? "create" : "update";
        string entityInformation = $"DailyMenuDto.Id={dailyMenuDto.Id};" +
                                   $"DailyMenuDto.MenuPrice={dailyMenuDto.MenuPrice}" +
                                   $"DailyMenuDto.DayOfWeek={dailyMenuDto.DayOfWeek}";
        var throwString = "";
        if (dailyMenuDto.Meal != null)
            throwString += $"Meal was set when attempting to {operation} daily menu!\n";
        if(dailyMenuDto.WeeklyMenu != null)
            throwString += $"WeeklyMenu was set when attempting to {operation} daily menu!\n";
        if (!string.IsNullOrEmpty(throwString))
            throw new InvalidOperationException(throwString + entityInformation);
    }

    public IEnumerable<DailyMenuDto> GetDailyMenusForWeeklyMenu(int weeklyMenuId)
    {
        var dailyMenuFilter = new DailyMenuFilterDto
        {
            WeeklyMenuId = weeklyMenuId
        };

        return _dailyMenuQueryObject.DailyMenusAssociatedToWeeklyMenu(dailyMenuFilter).Items;
    }
}