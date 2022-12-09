using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;

namespace PV179_RestaurantWeb.Controllers
{
    public class DrinkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDrinkService _drinkService;
        public DrinkController(IMapper mapper, IDrinkService drinkService)
        {
            _mapper = mapper;
            _drinkService = drinkService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<DrinkDto> drinkDtos = await _drinkService.GetAllAsync();
            var drinkViewModels = _mapper.Map<IEnumerable<DrinkViewModel>>(drinkDtos);
            return View(drinkViewModels);
        }

    }
}
