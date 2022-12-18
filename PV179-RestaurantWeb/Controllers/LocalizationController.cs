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

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<LocalizationDto> localizationDtos = await _localizationService.GetAllAsync();
            var localizationViewModels = _mapper.Map<IEnumerable<LocalizationCreateModel>>(localizationDtos);
            return View(localizationViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocalizationCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            LocalizationDto localizationDto = new LocalizationDto
            {
                IsoLanguageCode = model.IsoLanguageCode,
                StringCode = model.StringCode,
                LocalizedString = model.LocalizedString,
            };
            await _localizationService.CreateAsync(localizationDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _localizationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
