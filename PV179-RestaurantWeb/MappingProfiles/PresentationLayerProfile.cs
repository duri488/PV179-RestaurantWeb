using AutoMapper;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;

namespace PV179_RestaurantWeb.MappingProfiles;

public class PresentationLayerProfile : Profile
{
    public PresentationLayerProfile()
    {
        CreateMap<RestaurantViewModel, RestaurantDto>().ReverseMap();
        CreateMap<DailyMenuViewModel, DailyMenuDto>().ReverseMap();
        CreateMap<MealViewModel, MealDto>().ReverseMap();
        CreateMap<AllergenViewModel, AllergenDto>().ReverseMap();
    }
}