using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;

namespace PV179_RestaurantWeb.Controllers
{
    public class DailyMenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDailyMenuService _dailyMenuService;
        private readonly IAllergenService _allergenService;
        private readonly ILocalizationService _localizationService;
        public DailyMenuController(IMapper mapper, IDailyMenuService dailyMenuService, IAllergenService allergenService,
            ILocalizationService localizationService)
        {
            _mapper = mapper;
            _dailyMenuService = dailyMenuService;
            _allergenService = allergenService;
            _localizationService = localizationService;
        }

        // GET: DailyMenu
        public async Task<IActionResult> Index()
        {
            IEnumerable<DailyMenuDto> dailyMenuDtos = await _dailyMenuService.GetAllAsync(true);
            var dailyMenuViewModels = _mapper.Map<IEnumerable<DailyMenuViewModel>>(dailyMenuDtos);
            return View(dailyMenuViewModels);
        }

        // GET: DailyMenu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyMenuDto? dailyMenuDto = await _dailyMenuService.GetByIdAsync(id.Value, true);
            
            if (dailyMenuDto == null)
            {
                return NotFound();
            }

            var dailyMenu = _mapper.Map<DailyMenuViewModel>(dailyMenuDto);
            IEnumerable<AllergenDto> allergenDtos = await _allergenService.GetByFlags(dailyMenuDto.Meal.AllergenFlags);
            List<AllergenViewModel> allergens = LocalizeAllergens(allergenDtos).ToList();
            dailyMenu.Meal.Allergens = allergens;
            return View(dailyMenu);
        }

        private IEnumerable<AllergenViewModel> LocalizeAllergens(IEnumerable<AllergenDto> allergenDtos)
        {
            // TODO: How to get ISO code?
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
    }
}
