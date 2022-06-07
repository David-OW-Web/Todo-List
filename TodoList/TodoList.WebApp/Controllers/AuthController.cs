using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.WebApp.ViewModels;

namespace TodoList.WebApp.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        /// <summary>
        /// Zeigt die Ansicht an zum sich Registrieren
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Zeigt die Seite zum sich einloggen an
        /// </summary>
        /// <param name="returnUrl">Die URL auf die man nach erfolgreichem Login zurückgeschickt werden soll</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            LoginViewModel viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }
    }
}
