using AutoMapper;
using RestaurantWebBL.DTOs;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Configs
{
    public class BusinessMappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Drink, DrinkDto>().ReverseMap();
        }
    }
}
