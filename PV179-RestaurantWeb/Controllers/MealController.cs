using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;
using System.Text;
using System.Web;

namespace PV179_RestaurantWeb.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMealService _mealService;
        private readonly IAllergenService _allergenService;
        private readonly ILocalizationService _localizationService;
        public MealController(IMapper mapper, IMealService mealService, IAllergenService allergenService, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _mealService = mealService;
            _allergenService= allergenService;
            _localizationService = localizationService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<MealDto> mealDtos = await _mealService.GetAllAsync();
            var mealViewModels = _mapper.Map<IEnumerable<MealViewModel>>(mealDtos);
            return View(mealViewModels);
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
        
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MealDto? mealDto = await _mealService.GetByIdAsync(id.Value);

            if (mealDto == null)
            {
                return NotFound();
            }


            var meal = _mapper.Map<MealViewModel>(mealDto);
            
            IEnumerable<AllergenDto> allergenDtos = await _allergenService.GetByFlags(mealDto.AllergenFlags);
            List<AllergenViewModel> allergens = LocalizeAllergens(allergenDtos).ToList();
            meal.Allergens = allergens;
            return View(meal);

        }
        
        public async Task<IActionResult> Create()
        {
            //32767 sulphur-name iso en problem not incialize
            IEnumerable<AllergenDto> allergenDtos = await _allergenService.GetByFlags(127);
            List<AllergenViewModel> allergens = LocalizeAllergens(allergenDtos).ToList();
            ViewBag.Allergens = allergens;
            return View();
        }

        public int ConvertAllergens(string[] allergens)
        {
            int numberForAlergens = 0;
            try
            {
                for (int i = 0; i < allergens.Length; i++)
                {
                    numberForAlergens = (int)(numberForAlergens + Math.Pow(2, int.Parse(allergens[i]) - 1));
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return numberForAlergens;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MealCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string[] allergens = model.Allergens.Split(' ');
            int allergensNumbers = ConvertAllergens(allergens);

            if (allergensNumbers == -1)
            {
                return RedirectToAction(nameof(Index));
            }
            
            MealDto mealDto = new MealDto
            {
                Price = model.Price,
                Description = model.Description,
                Name = model.Name,
                Picture =model.Picture,
                AllergenFlags  = allergensNumbers,
            };
            
            await _mealService.CreateAsync(mealDto, 1);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MealDto? meal = await _mealService.GetByIdAsync(id.Value);

            if (meal == null)
            {
                return NotFound();
            }

            var AllergenNumbers = Convert.ToString(meal.AllergenFlags, 2);
            char[] CharArray = AllergenNumbers.ToCharArray();
            Array.Reverse(CharArray);
            var builder = new StringBuilder();
            int length = 1;
            foreach(char c in CharArray)
            {
                if (c == '1')
                {
                    builder.Append(length);
                    builder.Append(' ');
                }
                length = length + 1;               
            }
            builder.Length--;

            MealUpdateModel mealUpdateModel = new MealUpdateModel
            {
                Name = meal.Name,
                Price = meal.Price,
                Description = meal.Description,
                Picture = meal.Picture,
                Allergens = builder.ToString()
            };

            return View(mealUpdateModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(MealUpdateModel mealUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(mealUpdateModel);
            }

            string[] allergens = mealUpdateModel.Allergens.Split(' ');
            int allergensNumbers = ConvertAllergens(allergens);

            if (allergensNumbers == -1)
            {
                return RedirectToAction(nameof(Index));
            }

            var mealToUpdate = await _mealService.GetByIdAsync(mealUpdateModel.Id);

            mealToUpdate.Name = mealUpdateModel.Name;
            mealToUpdate.Price = mealUpdateModel.Price;
            mealToUpdate.Description = mealUpdateModel.Description;
            mealToUpdate.Picture = mealUpdateModel.Picture;
            mealToUpdate.AllergenFlags = allergensNumbers;

            await _mealService.UpdateAsync(mealToUpdate, 1);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _mealService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
