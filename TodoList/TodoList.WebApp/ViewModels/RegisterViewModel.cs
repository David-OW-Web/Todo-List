using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "E-Mail")]
        public string Email { get; set; } = String.Empty;
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
        [Display(Name = "Passwort wiederholen")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; } = String.Empty;
    }
}
