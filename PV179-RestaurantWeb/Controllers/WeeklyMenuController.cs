using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;

namespace PV179_RestaurantWeb.Controllers
{
    public class WeeklyMenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWeeklyMenuService _weeklyMenuService;
        private readonly IAllergenService _allergenService;
        private readonly IMenuFacade _menuFacade;
        private readonly ILocalizationService _localizationService;
        public WeeklyMenuController(IMapper mapper, IWeeklyMenuService weeklyMenuService, IAllergenService allergenService,
            IMenuFacade menuFacade, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _weeklyMenuService = weeklyMenuService;
            _allergenService = allergenService;
            _menuFacade = menuFacade;
            _localizationService = localizationService;
        }

        // GET: WeeklyMenu
        public async Task<IActionResult> Index()
        {
            List<WeeklyMenuDto> weeklyMenuDtos = (await _weeklyMenuService.GetAllAsync()).ToList();
            var weeklyMenuViewModels = _mapper.Map<List<WeeklyMenuViewModel>>(weeklyMenuDtos);

            foreach (WeeklyMenuViewModel weeklyMenuViewModel in weeklyMenuViewModels)
            {
                IEnumerable<DailyMenuDto> dailyMenusForCurrentWeek = 
                    await _menuFacade.GetDailyMenusForWeeklyMenu(weeklyMenuViewModel.Id!.Value, true);
                
                foreach (DailyMenuDto dailyMenuDto in dailyMenusForCurrentWeek)
                {
                    DailyMenuViewModel dailyMenu = await MapDailyMenuDtoToDailyMenuViewModel(dailyMenuDto);
                    weeklyMenuViewModel.DailyMenusEnumerable.Add(dailyMenu);
                }
                weeklyMenuViewModel.DailyMenusEnumerable.Sort((previous, next) =>
                        CompareDatesForSorting(previous.Date, next.Date, previous.Id, next.Id));
            }
            
            weeklyMenuViewModels.ToList().Sort((previous, next) =>
                CompareDatesForSorting(previous.DateFrom, next.DateFrom, previous.Id.Value, next.Id.Value));
            
            return View(weeklyMenuViewModels);
        }

        private static int CompareDatesForSorting(DateTime previous, DateTime next, int previousId, int nextId)
        {
            if (previous.Date > next.Date) return 1;
            if (previous.Date < next.Date) return -1;
            return previousId.CompareTo(nextId);
        }

        private async Task<DailyMenuViewModel> MapDailyMenuDtoToDailyMenuViewModel(DailyMenuDto dailyMenuDto)
        {
            var dailyMenu = _mapper.Map<DailyMenuViewModel>(dailyMenuDto);
            IEnumerable<AllergenDto> allergenDtos = await _allergenService.GetByFlags(dailyMenuDto.Meal.AllergenFlags);
            List<AllergenViewModel> allergens = LocalizeAllergens(allergenDtos).ToList();
            dailyMenu.Meal.Allergens = allergens;
            return dailyMenu;
        }

        // // GET: WeeklyMenu/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     DailyMenuDto? dailyMenuDto = await _weeklyMenuService.GetByIdAsync(id.Value, true);
        //     
        //     if (dailyMenuDto == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var dailyMenu = await MapDailyMenuDtoToDailyMenuViewModel(dailyMenuDto);
        //     return View(dailyMenu);
        // }

        private IEnumerable<AllergenViewModel> LocalizeAllergens(IEnumerable<AllergenDto> allergenDtos)
        {
            // TODO: How to get ISO code?
            string isoCode = "en";
            return allergenDtos.Select(a => new AllergenViewModel
            {
            
                Name = _localizationService.GetStringWithCode(isoCode, a.NameLocalizationCode)?.LocalizedString ?? 
                       throw new NotImplementedException($"Unable to find localization for" +
                                                         $"code:{a.NameLocalizationCode}; iso:{isoCode}"),
                Number = _localizationService.GetStringWithCode(isoCode, a.NumberLocalizationCode)?.LocalizedString ??
                         throw new NotImplementedException($"Unable to find localization for" +
                                                           $"code:{a.NumberLocalizationCode}; iso:{isoCode}")
            });
        }
    }
}
