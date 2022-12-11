using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PV179_RestaurantWeb.Models;
using RestaurantWebDAL.Models;

namespace PV179_RestaurantWeb.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;

        public IdentityController(SignInManager<User> signInManager,
            ILogger<IdentityController> logger,
            UserManager<User> userManager) : base()
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Login(string? returnUrl = "~/")
        {
            if (User.Identity?.Name is not null)
            {
                return Redirect(returnUrl!);
            }

            var viewModel = new LoginViewModel();
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false);

                if (user is null || await _userManager.CheckPasswordAsync(user, model.Password).ConfigureAwait(false) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);
                }

                await _signInManager.SignOutAsync().ConfigureAwait(false);
                var loggedIn = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false).ConfigureAwait(false);

                if (loggedIn.Succeeded)
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // This is not really the case, but we do not want the user to know the specifics of the error
                    ModelState.TryAddModelError("message", "Invalid credentials");
                    _logger.LogWarning($"Error logging in user {model.UserName}.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return Redirect(returnUrl);
        }
    }
}
