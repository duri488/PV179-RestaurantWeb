using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.Interfaces;

namespace PV179_RestaurantWeb.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantController(IMapper mapper, IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            var restaurant = restaurants.FirstOrDefault();

            var model = _mapper.Map<RestaurantViewModel>(restaurant);

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
