using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Services;

public class WeeklyMenuService : IWeeklyMenuService
{
    private readonly IRepository<WeeklyMenu> _weeklyMenuRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public WeeklyMenuService(IRepository<WeeklyMenu> weeklyMenuRepository,
        IMapper mapper,
        IUnitOfWorkFactory unitOfWorkFactory)
    {
        _weeklyMenuRepository = weeklyMenuRepository;
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task CreateAsync(WeeklyMenuDto createdEntity)
    {
        var weeklyMenu = _mapper.Map<WeeklyMenu>(createdEntity);
        await using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
        _weeklyMenuRepository.Insert(weeklyMenu);
        await unitOfWork.CommitAsync();
    }

    public async Task<WeeklyMenuDto?> GetByIdAsync(int entityId)
    {
        WeeklyMenu? dailyMenu = await _weeklyMenuRepository.GetByIdAsync(entityId);
        return _mapper.Map<WeeklyMenuDto?>(dailyMenu);
    }

    public async Task UpdateAsync(WeeklyMenuDto updatedEntity)
    {
        var updatedModel = _mapper.Map<WeeklyMenu>(updatedEntity);
        await using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
        _weeklyMenuRepository.Update(updatedModel);
        await unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int entityId)
    {
        await using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
        await _weeklyMenuRepository.DeleteAsync(entityId);
        await unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<WeeklyMenuDto>> GetAllAsync()
    {
        IEnumerable<WeeklyMenu> dailyMenuList = await _weeklyMenuRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<WeeklyMenuDto>>(dailyMenuList);
    }
}