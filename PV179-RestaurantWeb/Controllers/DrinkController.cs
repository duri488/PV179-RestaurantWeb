using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.Services;

namespace PV179_RestaurantWeb.Controllers
{
    public class DrinkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDrinkService _drinkService;
        private readonly IAllergenService _allergenService;
        public DrinkController(IMapper mapper, IDrinkService drinkService, IAllergenService allergenService)
        {
            _mapper = mapper;
            _drinkService = drinkService;
            _allergenService = allergenService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<DrinkDto> drinkDtos = await _drinkService.GetAllAsync();
            var drinkViewModels = _mapper.Map<IEnumerable<DrinkViewModel>>(drinkDtos);
            return View(drinkViewModels);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DrinkDto? drinkDto = await _drinkService.GetByIdAsync(id.Value);

            if (drinkDto == null)
            {
                return NotFound();
            }

            var drink = _mapper.Map<DrinkViewModel>(drinkDto);
            IEnumerable<AllergenDto> allergenDtos = await _allergenService.GetByFlags(drinkDto.AllergenFlags);
            var allergens = _mapper.Map<IEnumerable<AllergenViewModel>>(allergenDtos);
            drink.Allergens = allergens;
            return View(drink);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrinkCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DrinkDto drinkDto = new DrinkDto {
                Price = model.Price,
                Volume = model.Volume,
                Name = model.Name,
            };
            await _drinkService.CreateAsync(drinkDto, 1);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var dto = _drinkService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            _drinkService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
