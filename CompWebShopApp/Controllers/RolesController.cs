using AutoMapper;
using CompWebShopApp.Data;
using CompWebShopApp.Model.DTOs.Roles;
using CompWebShopApp.Model.DTOs.Users;
using CompWebShopApp.Model.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompWebShopApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ShopUser> userManager;
        private readonly IMapper mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ShopUser> userManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task <IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            IEnumerable<RoleDTO> rolesDTOs = mapper.Map<IEnumerable<RoleDTO>>(roles);
            return View(rolesDTOs);
        }
        
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError(string.Empty, "Введіть назву ролі");
                return View(model: roleName);
            }
            IdentityRole role = new IdentityRole{Name =roleName};
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");      
        }
        public async Task<IActionResult> UserList() 
        {
            IEnumerable<ShopUser> users = await userManager.Users.ToListAsync();
            IEnumerable<ShopUserDTO> usersDTOs = mapper.Map<IEnumerable<ShopUserDTO>>(users);
            return View(usersDTOs);    
        }
        public async Task<IActionResult> ChangeRoles(string id)
        {
           
            if (id == null)
                return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("Користувача не знайдено");
            var allRoles = await roleManager.Roles.ToListAsync();
            var userRoles = await userManager.GetRolesAsync(user);
           
            ChangeRolesVM changeRolesVM = new ChangeRolesVM()
            {
                Id = user.Id,
                Email = user.Email,
                AllRoles = allRoles,
                UserRoles = userRoles,
            };
            return View(changeRolesVM);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeRoles(ChangeRolesVM changeRolesVM)
        {
            ShopUser? user = await userManager.FindByIdAsync(changeRolesVM.Id);
            if (user == null) return NotFound();
            var allRoles = await roleManager.Roles.ToListAsync();

            var userRoles = await userManager.GetRolesAsync(user);
            if (ModelState.IsValid)
            {
                var addedRoles = changeRolesVM.Roles.Except(userRoles);
                var removedRoles = userRoles.Except(changeRolesVM.Roles);
                await userManager.AddToRolesAsync(user, addedRoles);
                await userManager.RemoveFromRolesAsync(user, removedRoles);
                return RedirectToAction("Index");
            }
            changeRolesVM.AllRoles = allRoles;
            changeRolesVM.UserRoles = userRoles;
            changeRolesVM.Email = user.Email;
            return View(changeRolesVM);
        }

        //public async Task<IActionResult> Edit(string id)
        //{
        //    IdentityRole? role = await roleManager.FindByIdAsync(id);
        //    if (role == null)
        //        return NotFound();
        //    RoleDTO roleDTO = mapper.Map<RoleDTO>(role);
        //    return View(roleDTO);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(RoleDTO roleDTO)
        //{
        //    if (!ModelState.IsValid)
        //        return View(roleDTO);
        //    IdentityRole? role = await roleManager.FindByIdAsync(roleDTO.Id);
        //    if (role != null)
        //    {
        //        role.Name = roleDTO.Name;
        //        await roleManager.UpdateAsync(role);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //        ModelState.AddModelError(string.Empty, "Ролі не знайдена");
        //    return View(roleDTO);
        //}

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();
            IdentityRole? result = await roleManager.FindByIdAsync(id);
            if(result == null) return NotFound();
            RoleDTO roleDTO = mapper.Map<RoleDTO>(result);
            return View(roleDTO);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (id == null) return NotFound();
            IdentityRole? role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }


    }
}
