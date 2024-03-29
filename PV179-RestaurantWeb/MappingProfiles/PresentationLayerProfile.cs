﻿using AutoMapper;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;

namespace PV179_RestaurantWeb.MappingProfiles;

public class PresentationLayerProfile : Profile
{
    public PresentationLayerProfile()
    {
        CreateMap<RestaurantViewModel, RestaurantDto>().ReverseMap();
        CreateMap<DailyMenuViewModel, DailyMenuDto>().ReverseMap();
        CreateMap<DailyMenuCreateModel, DailyMenuDto>().ReverseMap();
        CreateMap<WeeklyMenuViewModel, WeeklyMenuDto>().ReverseMap();
        CreateMap<WeeklyMenuCreateModel, WeeklyMenuDto>().ReverseMap();
        CreateMap<MealViewModel, MealDto>().ReverseMap();
        CreateMap<AllergenViewModel, AllergenDto>().ReverseMap();
        CreateMap<DrinkViewModel, DrinkDto>().ReverseMap();
        CreateMap<LocalizationCreateModel, LocalizationDto>().ReverseMap();
    }
}