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

    public async Task<IEnumerable<DailyMenu>> GetAllAsync(bool includeNavigationProperty)
    {
        return await _dbContext.Set<DailyMenu>()
            .Include(dm => dm.Meal)
            .Include(dm => dm.WeeklyMenu)
            .ToListAsync();
    }
}