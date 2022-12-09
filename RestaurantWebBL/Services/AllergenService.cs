using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWeb.Contract.Enums;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Services;

public class AllergenService : IAllergenService
{
    private readonly IRepository<Allergen> _repository;
    private readonly IMapper _mapper;
    public AllergenService(IRepository<Allergen> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AllergenDto>> GetByFlags(int bitFlags)
    {
        IEnumerable<Allergen> allergens = new List<Allergen>();
        foreach (Allergens allergen in (Allergens[]) Enum.GetValues(typeof(Allergens)))
        {
            if ((bitFlags & (int) allergen) == 0) continue;
            Allergen a = await _repository.GetByIdAsync((int) allergen) ??
                         throw new NotImplementedException($"Allergen '{allergen}' not found in database!");
            allergens = allergens.Append(a);
        }

        return _mapper.Map<IEnumerable<AllergenDto>>(allergens);
    }
}