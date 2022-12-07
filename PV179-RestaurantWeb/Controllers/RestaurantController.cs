using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWeb.Contract;
using RestaurantWebDAL.Models;
using RestaurantWebBL.Services;
using AutoMapper;

namespace PV179_RestaurantWeb.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRepository<Restaurant> _restaurantRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _mapper;

        public RestaurantController(IRepository<Restaurant> restaurantRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var restaurantService = new RestaurantService(_unitOfWorkFactory, _mapper, _restaurantRepository);

            var restaurants = await restaurantService.GetAllAsync();
            var restaurant = restaurants.FirstOrDefault();

            var model = new RestaurantViewModel()
            {
                Name = restaurant.Name,
                Address = restaurant.Address,
                Description = restaurant.Description,
                Latitude = restaurant.Latitude,
                Longtitude = restaurant.Longtitude,
                Phone = restaurant.Phone,
                Email = restaurant.Email
            };

            return View(model);
        }
    }
}
