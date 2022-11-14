using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDomain;

namespace RestaurantWebBL.Services;

public class DailyMenuService : IDailyMenuService
{
    private IRepository<DailyMenu> _dailyMenuRepository;
    public DailyMenuService(IRepository<DailyMenu> dailyMenuRepository)
    {
        _dailyMenuRepository = dailyMenuRepository;
    }

    public Task CreateAsync(DailyMenuDto createdEntity)
    {
        throw new NotImplementedException();
    }

    public Task<DailyMenuDto?> GetByIdAsync(int entityId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int entityId, DailyMenuDto updatedEntity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int entityId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DailyMenuDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}