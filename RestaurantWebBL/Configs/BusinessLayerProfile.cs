using AutoMapper;
using RestaurantWebBL.DTOs;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Configs
{
    public class BusinessLayerProfile
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<DailyMenu, DailyMenuDto>().ReverseMap();
            config.CreateMap<Drink, DrinkDto>().ReverseMap();
            config.CreateMap<Localization, LocalizationDto>().ReverseMap();
            config.CreateMap<Meal, MealDto>().ReverseMap();
            config.CreateMap<Restaurant, RestaurantDto>().ReverseMap();
            config.CreateMap<User, UserDto>().ReverseMap();
            config.CreateMap<WeeklyMenu, WeeklyMenuDto>().ReverseMap();
        }
    }
}
