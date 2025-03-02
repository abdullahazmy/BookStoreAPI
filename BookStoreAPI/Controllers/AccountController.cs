using BookStoreAPI.DTOs;
using BookStoreAPI.DTOs.AccountDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //Login
        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var r = signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, false, false).Result;
            if (r.Succeeded)
            {
                var user = userManager.FindByNameAsync(loginDTO.UserName).Result;

                if (user == null)
                {
                    return Unauthorized("Invalid UserName or Password");
                }

                //generate JWT tokens ....
                #region claims
                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim(ClaimTypes.Name, user.UserName ?? string.Empty));
                userdata.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty));

                var roles = userManager.GetRolesAsync(user).Result;
                foreach (var itemRole in roles)
                {
                    userdata.Add(new Claim(ClaimTypes.Role, itemRole));
                }
                #endregion

                #region secret key
                string key = "This is my custom secret key for authnetication";
                var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                #endregion

                var signingcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);

                #region generate token
                var token = new JwtSecurityToken(
                    claims: userdata,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingcer
                );

                //token object => encoded string
                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(tokenstring);
                #endregion
            }
            else
            {
                return Unauthorized("Invalid UserName or Password");
            }
        }

        //Logout
        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return Ok("Logout successfully");
        }

        //Change Password
        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByNameAsync(User.Identity.Name).Result;
                var r = userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword).Result;
                if (r.Succeeded)
                {
                    return Ok("Password changed successfully");
                }
                else
                {
                    return BadRequest(r.Errors);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}