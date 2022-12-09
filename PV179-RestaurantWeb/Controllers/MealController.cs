using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;

namespace PV179_RestaurantWeb.Controllers
{
    public class MealController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMealService _mealService;
        public MealController(IMapper mapper, IMealService mealService)
        {
            _mapper = mapper;
            _mealService = mealService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<MealDto> mealDtos = await _mealService.GetAllAsync();
            var mealViewModels = _mapper.Map<IEnumerable<MealViewModel>>(mealDtos);
            return View(mealViewModels);
        }

    }
}
