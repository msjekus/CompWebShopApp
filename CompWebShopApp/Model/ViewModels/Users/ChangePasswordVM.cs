using System.ComponentModel.DataAnnotations;

namespace CompWebShopApp.Model.ViewModels.Users
{
    public class ChangePasswordVM
    {
        public string Id { get; set; } = default!;
        public string? Email { get; set; } = default!;
        [Display(Name = "Старий пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = default!;
        [Display(Name = "Новий пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = default!;
        [Display(Name = "Підтвердіть новий пароль")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; } = default!;
    }
}
