using System.ComponentModel.DataAnnotations;

namespace CompWebShopApp.Model.DTOs.Admin
{
    public class LoginUserDTO
    {
   
        [Required]
        [Display(Name = "Логін")]
        public string UserName { get; set; } = default!;
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Display(Name = "Запам'ятати мене")]
        public bool RememberMe { get; set; }
    }
}
