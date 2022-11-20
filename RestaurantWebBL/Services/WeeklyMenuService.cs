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

    public async Task<int> UpsertAsync(WeeklyMenuDto weeklyMenuDto)
    {
        WeeklyMenu? weeklyMenu = await _weeklyMenuRepository.GetByIdAsync(weeklyMenuDto.Id);
        if (weeklyMenu is null)
        {
            weeklyMenuDto.Id = await CreateAsync(weeklyMenuDto);
            return weeklyMenuDto.Id;
        }

        await UpdateAsync(weeklyMenuDto);
        return weeklyMenuDto.Id;
    }

    public async Task<int> CreateAsync(WeeklyMenuDto createdEntity)
    {
        var weeklyMenu = _mapper.Map<WeeklyMenu>(createdEntity);
        await using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
        int id = _weeklyMenuRepository.Insert(weeklyMenu);
        await unitOfWork.CommitAsync();
        return id;
    }

    public async Task<WeeklyMenuDto?> GetByIdAsync(int entityId)
    {
        WeeklyMenu? dailyMenu = await _weeklyMenuRepository.GetByIdAsync(entityId);
        return _mapper.Map<WeeklyMenuDto?>(dailyMenu);
    }

    public async Task UpdateAsync(WeeklyMenuDto updatedEntity)
    {
        WeeklyMenu weeklyMenu = await _weeklyMenuRepository.GetByIdAsync(updatedEntity.Id) ??
                                 throw new InvalidOperationException($"Entity with id {updatedEntity.Id} does not exist!");;
        weeklyMenu.DateFrom = updatedEntity.DateFrom;
        weeklyMenu.DateTo = updatedEntity.DateTo;
        await using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
        _weeklyMenuRepository.Update(weeklyMenu);
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