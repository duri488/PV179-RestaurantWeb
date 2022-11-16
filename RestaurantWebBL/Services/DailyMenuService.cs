using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Services;

public class DailyMenuService : IDailyMenuService
{
    private readonly IRepository<DailyMenu> _dailyMenuRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public DailyMenuService(IRepository<DailyMenu> dailyMenuRepository,
        IMapper mapper,
        IUnitOfWorkFactory unitOfWorkFactory)
    {
        _dailyMenuRepository = dailyMenuRepository;
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task CreateAsync(DailyMenuDto createdEntity)
    {
        var dailyMenu = _mapper.Map<DailyMenu>(createdEntity);
        await using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
        _dailyMenuRepository.Insert(dailyMenu);
        await unitOfWork.CommitAsync();
    }

    public async Task<DailyMenuDto?> GetByIdAsync(int entityId)
    {
        DailyMenu? dailyMenu = await _dailyMenuRepository.GetByIdAsync(entityId);
        return _mapper.Map<DailyMenuDto?>(dailyMenu);
    }

    public async Task UpdateAsync(DailyMenuUpdateDto updatedEntity)
    {
        DailyMenu dailyMenu = await _dailyMenuRepository.GetByIdAsync(updatedEntity.Id) ?? 
                              throw new InvalidOperationException($"Entity with id {updatedEntity.Id} does not exist!");
        dailyMenu.MenuPrice = updatedEntity.MenuPrice;
        dailyMenu.DayOfWeek = updatedEntity.DayOfWeek;
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
        IEnumerable<DailyMenu> dailyMenuList = await _dailyMenuRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DailyMenuDto>>(dailyMenuList);
    }
}