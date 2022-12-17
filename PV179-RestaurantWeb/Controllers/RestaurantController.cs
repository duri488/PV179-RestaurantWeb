using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
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
            var restaurant = await _restaurantService.GetFirstAsync();
            var model = _mapper.Map<RestaurantViewModel>(restaurant);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUsMail(RestaurantViewModel model)
        {
            await SendMail(model.ContactName, model.ContactMail, model.ContactMessage);
            return RedirectToAction(nameof(Index));
        }

        private async Task SendMail(string name, string email, string msg)
        {
            var restaurant = await _restaurantService.GetFirstAsync();
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress(email);
            message.To.Add(restaurant.Email);
            message.Subject = "Contact form from: " + name;
            message.IsBodyHtml = true;
            message.Body = "<p>Name: " + name + "</p>" + "<p>Email: " + email + "</p>" + "<p>Message: " + msg + "</p>";

            //should be configured when deployed
            smtpClient.Port = 25;
            smtpClient.Host = "192.168.1.13";
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = true;
            //smtpClient.Credentials = new NetworkCredential("Username", "Password");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtpClient.SendMailAsync(message);
        }
    }
}
