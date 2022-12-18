using AutoMapper;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;

namespace PV179_RestaurantWeb.MappingProfiles;

public class PresentationLayerProfile : Profile
{
    public PresentationLayerProfile()
    {
        CreateMap<RestaurantViewModel, RestaurantDto>().ReverseMap();
       // CreateMap<UserViewModel, UserDto>().ReverseMap();
        CreateMap<DailyMenuViewModel, DailyMenuDto>().ReverseMap();
        CreateMap<WeeklyMenuViewModel, WeeklyMenuDto>().ReverseMap();
        CreateMap<MealViewModel, MealDto>().ReverseMap();
        CreateMap<AllergenViewModel, AllergenDto>().ReverseMap();
        CreateMap<DrinkViewModel, DrinkDto>().ReverseMap();
        //CreateMap<DrinkCreateModel, DrinkDto>().ReverseMap();
    }
}