using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace PV179_RestaurantWeb.Controllers
{
    [Authorize]
    public class WeeklyMenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWeeklyMenuService _weeklyMenuService;
        private readonly IAllergenService _allergenService;
        private readonly IMenuFacade _menuFacade;
        private readonly ILocalizationService _localizationService;
        private readonly SignInManager<User> _signInManager;
        public WeeklyMenuController(IMapper mapper, IWeeklyMenuService weeklyMenuService, IAllergenService allergenService,
            IMenuFacade menuFacade, ILocalizationService localizationService, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _weeklyMenuService = weeklyMenuService;
            _allergenService = allergenService;
            _menuFacade = menuFacade;
            _localizationService = localizationService;
            _signInManager = signInManager;
        }

        // GET: WeeklyMenu
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<WeeklyMenuViewModel> weeklyMenuViewModels;
            if (_signInManager.IsSignedIn(User))
            {
                 weeklyMenuViewModels = await GetAdminWeeklyMenus();
            }
            else
            {
                weeklyMenuViewModels = await GetUserWeeklyMenus();
            }

            await PopulateNavigationalProperties(weeklyMenuViewModels);
            
            weeklyMenuViewModels.Sort((previous, next) =>
                CompareDatesForSorting(previous.DateFrom, next.DateFrom, previous.Id.Value, next.Id.Value));
            
            return View(weeklyMenuViewModels);
        }

        private async Task PopulateNavigationalProperties(List<WeeklyMenuViewModel> weeklyMenuViewModels)
        {
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
        }

        private async Task<List<WeeklyMenuViewModel>> GetAdminWeeklyMenus()
        {
            List<WeeklyMenuDto> weeklyMenuDtos = (await _weeklyMenuService.GetAllAsync()).ToList();
            var weeklyMenuViewModels = _mapper.Map<List<WeeklyMenuViewModel>>(weeklyMenuDtos);
            return weeklyMenuViewModels;
        }

        private async Task<List<WeeklyMenuViewModel>> GetUserWeeklyMenus()
        {
            List<WeeklyMenuDto> weeklyMenuDtos = _weeklyMenuService.GetWeeklyMenusByDate(DateTime.Today).ToList();
            var weeklyMenuViewModels = _mapper.Map<List<WeeklyMenuViewModel>>(weeklyMenuDtos);
            return weeklyMenuViewModels;
        }

        private static int CompareDatesForSorting(DateTime previous, DateTime next, int previousId, int nextId)
        {
            if (previous != next) return previous.CompareTo(next);
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

        private IEnumerable<AllergenViewModel> LocalizeAllergens(IEnumerable<AllergenDto> allergenDtos)
        {
            return allergenDtos.Select(a => new AllergenViewModel
            {
            
                Name = _localizationService.GetStringWithCode(a.NameLocalizationCode),
                Number = _localizationService.GetStringWithCode(a.NumberLocalizationCode)
            });
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WeeklyMenuCreateModel weeklyMenu)
        {
            if (!ModelState.IsValid) return View();
            
            var weeklyMenuDto = _mapper.Map<WeeklyMenuDto>(weeklyMenu);
            await _weeklyMenuService.CreateAsync(weeklyMenuDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult VerifyDateRange(DateTime dateFrom, DateTime dateTo)
        {
            if (dateTo < dateFrom)
            {
                return Json("Date from must be later than date to");
            }
            if (dateTo - dateFrom != TimeSpan.FromDays(7))
            {
                return Json("Weekly menu must be 7 days long");
            }

            return Json(true);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var dailyMenuDtos = await _menuFacade.GetDailyMenusForWeeklyMenu(id, false);
            foreach (DailyMenuDto dailyMenuDto in dailyMenuDtos)
            {
                await _menuFacade.DeleteDailyMenuAsync(dailyMenuDto.Id);
            }

            await _weeklyMenuService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
