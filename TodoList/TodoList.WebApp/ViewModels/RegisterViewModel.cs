using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessage = "Bitte eine gültige E-Mail Adresse eingeben")]
        [Required(ErrorMessage = "{0} ist Pflichtfeld")]
        public string Email { get; set; } = String.Empty;
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} ist Pflichtfeld")]
        public string Password { get; set; } = String.Empty;
        [Display(Name = "Passwort wiederholen")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} ist Pflichtfeld")]
        [Compare("Password", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string RepeatPassword { get; set; } = String.Empty;
    }
}
