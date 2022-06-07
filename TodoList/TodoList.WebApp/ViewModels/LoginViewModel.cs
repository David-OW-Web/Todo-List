using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "E-Mail")]
        public string Email { get; set; } = String.Empty;
        [Display(Name = "Passwort")]
        public string Password { get; set; } = String.Empty;
        [Display(Name = "Eingeloggt bleiben?")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
