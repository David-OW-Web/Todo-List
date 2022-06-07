using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data.Models.Auth;
using TodoList.WebApp.ViewModels;

namespace TodoList.WebApp.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
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
        /// Verarbeitet die Daten beim Registrieren
        /// </summary>
        /// <param name="model">Instanz eines Modells mit den Daten</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                // Mappe die Daten vom ViewModel zu einem ApplicationUser-Modell
                ApplicationUser newUser = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                // Versuche den Benutzer zu erstellen
                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

                // Prüfe ob Fehler enststanden sind
                if(!result.Succeeded)
                {
                    // Füge Fehler zum Modell hinzu (Englisch, keine Übersetzung)
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
                // Alles ist gut. Logge Benutzer ein und schicke ihm zur Listenübersicht aller Todo-Listen
                await _signInManager.SignInAsync(newUser, true);
                return RedirectToAction("Index", "TodoList");
            }
            return View(model);
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
