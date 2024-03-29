using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;

namespace PV179_RestaurantWeb.Controllers
{
    [Authorize]
    public class DailyMenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDailyMenuService _dailyMenuService;
        private readonly IAllergenService _allergenService;
        private readonly IMenuFacade _menuFacade;
        private readonly ILocalizationService _localizationService;
        public DailyMenuController(IMapper mapper, IDailyMenuService dailyMenuService, IAllergenService allergenService,
            IMenuFacade menuFacade, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _dailyMenuService = dailyMenuService;
            _allergenService = allergenService;
            _menuFacade = menuFacade;
            _localizationService = localizationService;
        }

        // GET: DailyMenu
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<DailyMenuDto> dailyMenuDtos = await _dailyMenuService.GetAllAsync(true);
            var dailyMenuViewModels = _mapper.Map<IEnumerable<DailyMenuViewModel>>(dailyMenuDtos);
            return View(dailyMenuViewModels);
        }

        // GET: DailyMenu/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyMenuViewModel? dailyMenu = await GetDetailViewModelById(id);
            if (dailyMenu == null)
            {
                return NotFound();
            }
            
            return View(dailyMenu);
        }

        private async Task<DailyMenuViewModel?> GetDetailViewModelById([DisallowNull] int? id)
        {
            DailyMenuDto? dailyMenuDto = await _dailyMenuService.GetByIdAsync(id.Value, true);

            if (dailyMenuDto == null)
            {
                return null;
            }

            var dailyMenu = _mapper.Map<DailyMenuViewModel>(dailyMenuDto);
            IEnumerable<AllergenDto> allergenDtos = await _allergenService.GetByFlags(dailyMenuDto.Meal.AllergenFlags);
            List<AllergenViewModel> allergens = LocalizeAllergens(allergenDtos).ToList();
            dailyMenu.Meal.Allergens = allergens;
            return dailyMenu;
        }

        private IEnumerable<AllergenViewModel> LocalizeAllergens(IEnumerable<AllergenDto> allergenDtos)
        {
            // TODO: How to get ISO code?
            string isoCode = "en";
            return allergenDtos.Select(a => new AllergenViewModel
            {
            
                Name = _localizationService.GetStringWithCode(a.NameLocalizationCode),
                Number = _localizationService.GetStringWithCode(a.NumberLocalizationCode)
            });
        }

        // GET: DailyMenu/Create
        public async Task<IActionResult> Create()
        {
            var dailyMenuCreateModel = new DailyMenuCreateModel();
            await PopulateDailyMenuCreateModelEnumerables(dailyMenuCreateModel);
            dailyMenuCreateModel.DayOfWeek = new DayOfWeek();
            return View(dailyMenuCreateModel);
        }

        private async Task PopulateDailyMenuCreateModelEnumerables(DailyMenuCreateModel dailyMenuCreateModel)
        {
            dailyMenuCreateModel.MealsEnumerable = await _menuFacade.GetAllMealsAsync();
            dailyMenuCreateModel.WeeklyMenusEnumerable = _menuFacade.GetWeeklyMenusByDate(DateTime.Today);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DailyMenuCreateModel dailyMenu)
        {
            if (ModelState.IsValid)
            {
                var dailyMenuDto = new DailyMenuDto
                {
                    DayOfWeek = dailyMenu.DayOfWeek!.Value,
                    MenuPrice = dailyMenu.MenuPrice
                };
                
                await _dailyMenuService.CreateAsync(dailyMenuDto,dailyMenu.MealId, dailyMenu.WeeklyMenuId);
                return RedirectToAction(nameof(Index));
            }
            var dailyMenuCreateModel = new DailyMenuCreateModel();
            await PopulateDailyMenuCreateModelEnumerables(dailyMenuCreateModel);
            return View(dailyMenuCreateModel);
        }

        // GET: DailyMenu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyMenuViewModel? dailyMenu = await GetDetailViewModelById(id);
            
            if (dailyMenu == null)
            {
                return NotFound();
            }
            
            return View(dailyMenu);
        }
        
        // POST: DailyMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _dailyMenuService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        // GET: DailyMenu/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            
            var dailyMenuCreateModel = _mapper.Map<DailyMenuCreateModel>(dailyMenuDto);
            await PopulateDailyMenuCreateModelEnumerables(dailyMenuCreateModel);
            
            return View(dailyMenuCreateModel);
        }

        // POST: DailyMenu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DailyMenuCreateModel dailyMenuCreateModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDailyMenuCreateModelEnumerables(dailyMenuCreateModel);
                return View(dailyMenuCreateModel);
            }
            
            DailyMenuDto? dailyMenuDto = await _dailyMenuService.GetByIdAsync(id);
            
            if (dailyMenuDto == null)
            {
                return NotFound();
            }

            dailyMenuDto = _mapper.Map<DailyMenuDto>(dailyMenuCreateModel);
                
            await _dailyMenuService.UpdateAsync(dailyMenuDto, dailyMenuCreateModel.MealId,
                dailyMenuCreateModel.WeeklyMenuId);
                
            
            return RedirectToAction(nameof(Index));

        }
    }
}
