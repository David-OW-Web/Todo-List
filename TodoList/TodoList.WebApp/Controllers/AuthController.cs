using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data.Models.Auth;
using TodoList.WebApp.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

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

        /// <summary>
        /// Verarbeitet das Login
        /// </summary>
        /// <param name="model">Ausgefülltes Model mit Benutzerdaten</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                // Prüfen ob der Benutzer existiert
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if(user == null)
                {
                    ModelState.AddModelError("Email", "Ein unerwarteter Fehler ist aufgetreten");
                    return View(model);
                }

                SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                // Prüfe ob es Fehler gibt
                if(!result.Succeeded)
                {
                    ModelState.AddModelError("Email", "Es sind Fehler beim Login aufgetreten");
                    return View(model);
                }

                // Es war alles gut. Leite Benutzer zu seinen Listen weiter oder zur ReturnUrl

                if(!String.IsNullOrEmpty(model.ReturnUrl))
                {
                    return LocalRedirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "TodoList");
            }
            return View(model);
        }

        /// <summary>
        /// Loggt den Benutzer aus und schickt ihn zurück zum Login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
