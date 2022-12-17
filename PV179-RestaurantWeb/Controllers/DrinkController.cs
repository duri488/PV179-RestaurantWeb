using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Facades;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;

namespace PV179_RestaurantWeb.Controllers
{
    public class DrinkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDrinkService _drinkService;
        private readonly IAllergenService _allergenService;
        private readonly ILocalizationService _localizationService;
        public DrinkController(IMapper mapper, IDrinkService drinkService, IAllergenService allergenService, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _drinkService = drinkService;
            _allergenService = allergenService;
            _localizationService = localizationService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<DrinkDto> drinkDtos = await _drinkService.GetAllAsync();
            var drinkViewModels = _mapper.Map<IEnumerable<DrinkViewModel>>(drinkDtos);
            return View(drinkViewModels);
        }

        private IEnumerable<AllergenViewModel> LocalizeAllergens(IEnumerable<AllergenDto> allergenDtos)
        {
            string isoCode = "en";
            return allergenDtos.Select(a => new AllergenViewModel
            {

                Name = _localizationService.GetStringWithCode(isoCode, a.NameLocalizationCode)?.LocalizedString ??
                       throw new NotImplementedException($"Unable to find localization string for " +
                                                         $"code:{a.NameLocalizationCode}; iso:{isoCode}"),
                Number = _localizationService.GetStringWithCode(isoCode, a.NumberLocalizationCode)?.LocalizedString ??
                         throw new NotImplementedException($"Unable to find localization string for " +
                                                           $"code:{a.NumberLocalizationCode}; iso:{isoCode}")
            });
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
            /*
            IEnumerable<AllergenDto> allergenDtos = await _allergenService.GetByFlags(drinkDto.AllergenFlags);
            List<AllergenViewModel> allergens = LocalizeAllergens(allergenDtos).ToList();
            drink.Allergens = allergens;*/
            return View(drink);
            
        }

        public async Task<IActionResult> Create()
        {
            //IEnumerable<AllergenDto> allergens = await _allergenService.GetByFlags(127);
            //ViewBag.Allergens = allergens;
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
                //AllergenFlags= 65,
               
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
                 //Allergens = drink.AllergenFlags
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

            var dto = _drinkService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            // pise ze je otvorena transakcia a ze sa musi ukoncit pred ty ale ked ju debagujem tak to prejde vsetko ok a neni problem 
            // System.InvalidOperationException: There is already an open DataReader associated with this Connection which must be closed first.
            //await _drinkService.DeleteAsync(id);

            _drinkService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
