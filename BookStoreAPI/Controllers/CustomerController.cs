using BookStoreAPI.DTOs;
using BookStoreAPI.DTOs.CustomerDTO;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public CustomerController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult SelecAllCustomers()
        {
            var customers = userManager.GetUsersInRoleAsync("Customer").Result.OfType<Customer>().ToList();
            if (!customers.Any()) return NotFound();
            List<SelectAllCustomersDTO> customersDTO = new List<SelectAllCustomersDTO>();
            foreach (var cust in customers)
            {
                SelectAllCustomersDTO cDTO = new SelectAllCustomersDTO()
                {
                    Id = cust.Id,
                    UserName = cust.UserName,
                    FullName = cust.FullName,
                    Email = cust.Email,
                    PhoneNumber = cust.PhoneNumber,
                    Address = cust.Address
                };
                customersDTO.Add(cDTO);
            }
            return Ok(customersDTO);
        }

        [HttpGet("{id}")]
        public IActionResult SelectCustomerById(string id)
        {
            var cust = (Customer)userManager.GetUsersInRoleAsync("Customer").Result.FirstOrDefault(c => c.Id == id);
            if (cust == null) return NotFound();
            SelectAllCustomersDTO cDTO = new SelectAllCustomersDTO()
            {
                Id = cust.Id,
                UserName = cust.UserName,
                FullName = cust.FullName,
                Email = cust.Email,
                PhoneNumber = cust.PhoneNumber,
                Address = cust.Address
            };
            return Ok(cDTO);
        }

        [HttpPost]
        public IActionResult AddCustomer(AddCustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                var cust = new Customer()
                {
                    UserName = customerDTO.UserName,
                    FullName = customerDTO.FullName,
                    Email = customerDTO.Email,
                    PhoneNumber = customerDTO.PhoneNumber,
                    Address = customerDTO.Address
                };
                var result = userManager.CreateAsync(cust, customerDTO.Password).Result;

                if (result.Succeeded)
                {
                    IdentityRole _role = roleManager.FindByNameAsync("Customer").Result;
                    if (_role != null)
                    {
                        IdentityResult roleResult = userManager.AddToRoleAsync(cust, _role.Name).Result;
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
        public IActionResult EditCustomer(EditCustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                var cust = (Customer)userManager.FindByIdAsync(customerDTO.Id).Result;
                if (cust != null)
                {
                    cust.FullName = customerDTO.FullName;
                    cust.Email = customerDTO.Email;
                    cust.PhoneNumber = customerDTO.PhoneNumber;
                    cust.Address = customerDTO.Address;
                    cust.UserName = customerDTO.UserName;

                    var result = userManager.UpdateAsync(cust).Result;
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
        public IActionResult ChangeCustomerPassword(ChangePasswordDTO changePasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var cust = (Customer)userManager.FindByIdAsync(changePasswordDTO.Id).Result;
                if (cust != null)
                {
                    var result = userManager.ChangePasswordAsync(cust, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword).Result;
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
