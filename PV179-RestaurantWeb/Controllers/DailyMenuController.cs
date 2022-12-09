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

        public DailyMenuController(IMapper mapper, IDailyMenuService dailyMenuService)
        {
            _mapper = mapper;
            _dailyMenuService = dailyMenuService;
        }

        // GET: DailyMenu
        public async Task<IActionResult> Index()
        {
            IEnumerable<DailyMenuDto> dailyMenuDtos = await _dailyMenuService.GetAllAsync(true);
            var dailyMenuViewModels = _mapper.Map<IEnumerable<DailyMenuViewModel>>(dailyMenuDtos);
            return View(dailyMenuViewModels);
        }
    }
}
