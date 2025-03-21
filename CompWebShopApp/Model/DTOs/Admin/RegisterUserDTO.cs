using System.ComponentModel.DataAnnotations;

namespace CompWebShopApp.Model.DTOs.Admin
{
    public class RegisterUserDTO
    {
        [Required]
        [Display(Name = "Логін")]
        public string UserName { get; set; } = default!;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [Required]
        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; } = default!;
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        [Required]
        [Display(Name = "Підтвердження пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
