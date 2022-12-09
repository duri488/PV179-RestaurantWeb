using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Contract;
using RestaurantWebDAL;
using RestaurantWebDAL.Models;

namespace RestaurantWebInfrastructure.EFCore.Repository;

public class EfDailyMenuRepository : EfRepository<DailyMenu>, IEagerLoadingRepository<DailyMenu>
{
    public EfDailyMenuRepository(RestaurantWebDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<DailyMenu?> GetByIdAsync(int id, bool includeNavigationProperty)
    {
        if (!includeNavigationProperty) return await GetByIdAsync(id);
        
        return await _dbContext.Set<DailyMenu>()
            .Include(dm => dm.Meal)
            .Include(dm => dm.WeeklyMenu)
            .FirstOrDefaultAsync(dm => dm.Id == id);
    }

    public async Task<IEnumerable<DailyMenu>> GetAllAsync(bool includeNavigationProperty)
    {
        if (!includeNavigationProperty) return await GetAllAsync();

        return await _dbContext.Set<DailyMenu>()
            .Include(dm => dm.Meal)
            .Include(dm => dm.WeeklyMenu)
            .ToListAsync();
    }
}