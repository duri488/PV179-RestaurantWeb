using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.Services;

namespace PV179_RestaurantWeb.Controllers
{
    [Authorize]
    public class LocalizationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public LocalizationController(IMapper mapper, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }


    }
}
