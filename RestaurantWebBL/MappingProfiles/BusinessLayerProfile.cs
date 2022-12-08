using AutoMapper;
using RestaurantWebBL.DTOs;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.MappingProfiles
{
    public class BusinessLayerProfile : Profile
    {
        public BusinessLayerProfile()
        {
            CreateMap<DailyMenu, DailyMenuDto>().ReverseMap();
            CreateMap<Drink, DrinkDto>().ReverseMap();
            CreateMap<Localization, LocalizationDto>().ReverseMap();
            CreateMap<Meal, MealDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<WeeklyMenu, WeeklyMenuDto>().ReverseMap();
        }
    }
}
