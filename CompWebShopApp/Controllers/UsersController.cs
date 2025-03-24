using AutoMapper;
using CompWebShopApp.Data;
using CompWebShopApp.Model.DTOs.Users;
using CompWebShopApp.Model.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CompWebShopApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly IMapper mapper;

        public UsersController(UserManager<ShopUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task <IActionResult> Index()
        {
            IEnumerable<ShopUser> users = await userManager.Users.ToListAsync();
            IEnumerable<ShopUserDTO> usersDTOs = mapper.Map<IEnumerable<ShopUserDTO>>(users);
            return View(usersDTOs);
        }
        public async Task <IActionResult> Edit(string? id)
        { 
            if (id == null)
                return NotFound();
            ShopUser? user= await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("Користувача не знайдено");
            ShopUserDTO userDTO = mapper.Map<ShopUserDTO>(user);
            return View(userDTO);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(ShopUserDTO userDTO)
        {
            if(!ModelState.IsValid)
                return View(userDTO);
            ShopUser? user = await userManager.FindByIdAsync( userDTO.Id);
            if (user != null)
            {
                user.DateOfBirth = userDTO.DateOfBirth;
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
               foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            else
                ModelState.AddModelError(string.Empty, "Користувача не знайдено");
            return View(userDTO);
        }
        public async Task<IActionResult> ChangePassword(string? id)
        {
            if (id == null)
                return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("Користувача не знайдено");
            ChangePasswordVM vM = new ChangePasswordVM {
                Id = user.Id,
                Email = user.Email 
            };
            return View(vM);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM vM)
        {
            if (!ModelState.IsValid)
                return View(vM);
            ShopUser? user = await userManager.FindByIdAsync(vM.Id);
            if(user == null) return NotFound();
            var res=await userManager.ChangePasswordAsync(user, vM.OldPassword, vM.NewPassword);
            if (res.Succeeded) return RedirectToAction("Index");
            foreach (var error in res.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(vM);
        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
                return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound("Користувача не знайдено");
            ShopUserDTO userDTO = mapper.Map<ShopUserDTO>(user);
            return View(userDTO);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(string? Id)
        {
            if (Id == null) return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(Id);
            if (user == null) return NotFound("Користувача не знайдено");
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

    }
}
