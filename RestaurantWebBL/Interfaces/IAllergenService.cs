using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces;

public interface IAllergenService
{
    Task<IEnumerable<AllergenDto>> GetByFlags(int bitFlags);
}