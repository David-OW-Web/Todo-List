using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessage = "Bitte eine gültige E-Mail Adresse eingeben")]
        [Required(ErrorMessage = "{0} ist Pflichtfeld")]
        public string Email { get; set; } = String.Empty;
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} ist Pflichtfeld")]
        public string Password { get; set; } = String.Empty;
        [Display(Name = "Eingeloggt bleiben?")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
