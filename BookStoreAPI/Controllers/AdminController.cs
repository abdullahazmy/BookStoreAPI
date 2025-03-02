using BookStoreAPI.DTOs;
using BookStoreAPI.DTOs.AdminDTOs;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult SelecAllAdmins()
        {
            var admins = userManager.GetUsersInRoleAsync("Admin").Result.OfType<Admin>().ToList();
            if (!admins.Any()) return NotFound();
            List<SelectAllAdminsDTO> adminDTO = new List<SelectAllAdminsDTO>();
            foreach (var admin in admins)
            {
                SelectAllAdminsDTO admDTO = new SelectAllAdminsDTO()
                {
                    Id = admin.Id,
                    UserName = admin.UserName,
                    Email = admin.Email,
                    PhoneNumber = admin.PhoneNumber,
                };
                adminDTO.Add(admDTO);
            }
            return Ok(adminDTO);
        }

        [HttpGet("{id}")]
        public IActionResult SelectAdminById(string id)
        {
            var admin = (Admin)userManager.GetUsersInRoleAsync("Admin").Result.FirstOrDefault(c => c.Id == id);
            if (admin == null) return NotFound();
            SelectAllAdminsDTO admDTO = new SelectAllAdminsDTO()
            {
                Id = admin.Id,
                UserName = admin.UserName,
                Email = admin.Email,
                PhoneNumber = admin.PhoneNumber,
            };
            return Ok(admDTO);
        }

        [HttpPost]
        public IActionResult AddAdmin(AddAdminDTO addAdminDTO)
        {
            if (ModelState.IsValid)
            {
                var admin = new Admin()
                {
                    UserName = addAdminDTO.UserName,
                    Email = addAdminDTO.Email,
                    PhoneNumber = addAdminDTO.PhoneNumber
                };
                var result = userManager.CreateAsync(admin, addAdminDTO.Password).Result;

                if (result.Succeeded)
                {
                    IdentityRole _role = roleManager.FindByNameAsync("Admin").Result;
                    if (_role != null)
                    {
                        IdentityResult roleResult = userManager.AddToRoleAsync(admin, _role.Name).Result;
                        if (roleResult.Succeeded)
                            return Ok();
                        else
                            return BadRequest(roleResult.Errors);
                    }
                    else
                    {
                        return BadRequest("Role not found");
                    }
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult EditAdmin(EditAdminDTO editAdminDTO)
        {
            if (ModelState.IsValid)
            {
                var admin = (Admin)userManager.FindByIdAsync(editAdminDTO.Id).Result;
                if (admin != null)
                {
                    admin.UserName = editAdminDTO.UserName;
                    admin.Email = editAdminDTO.Email;
                    admin.PhoneNumber = editAdminDTO.PhoneNumber;

                    var result = userManager.UpdateAsync(admin).Result;
                    if (result.Succeeded)
                        return NoContent();
                    else
                        return BadRequest(result.Errors);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult ChangeAdminPassword(ChangePasswordDTO changePasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var admin = (Admin)userManager.FindByIdAsync(changePasswordDTO.Id).Result;
                if (admin != null)
                {
                    var result = userManager.ChangePasswordAsync(admin, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword).Result;
                    if (result.Succeeded)
                        return NoContent();
                    else
                        return BadRequest(result.Errors);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
