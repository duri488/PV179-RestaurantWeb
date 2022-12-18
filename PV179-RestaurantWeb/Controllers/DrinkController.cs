using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;

namespace PV179_RestaurantWeb.Controllers
{
    [Authorize]
    public class DrinkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDrinkService _drinkService;
        private readonly ILocalizationService _localizationService;
        public DrinkController(IMapper mapper, IDrinkService drinkService, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _drinkService = drinkService;
            _localizationService = localizationService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<DrinkDto> drinkDtos = await _drinkService.GetAllAsync();
            var drinkViewModels = _mapper.Map<IEnumerable<DrinkViewModel>>(drinkDtos);
            return View(drinkViewModels);
        }

        [AllowAnonymous]
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
            return View(drink);
            
        }

        public async Task<IActionResult> Create()
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

        public async Task<ActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DrinkDto? drink = await _drinkService.GetByIdAsync(id.Value);

            if (drink == null)
            {
                return NotFound();
            }
            DrinkUpdateModel drinkUpdateModel = new DrinkUpdateModel
            {
                 Name = drink.Name,
                 Price = drink.Price,
                 Volume = drink.Volume,
            };
            return View(drinkUpdateModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(DrinkUpdateModel drinkUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(drinkUpdateModel);
            }


            var drinkToUpdate = await _drinkService.GetByIdAsync(drinkUpdateModel.Id);

            drinkToUpdate.Name = drinkUpdateModel.Name;
            drinkToUpdate.Price = drinkUpdateModel.Price;
            drinkToUpdate.Volume = drinkUpdateModel.Volume;
            await _drinkService.UpdateAsync(drinkToUpdate,1);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _drinkService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
