using System.ComponentModel.DataAnnotations;

namespace CompWebShopApp.Model.DTOs.Roles
{
    public class RoleDTO
    {
        public string Id { get; set; } = default!;
        [Display(Name = "Назва ролі")]
        public string Name { get; set; } = default!;
    }
}
