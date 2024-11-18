using ChromeHeart.Areas.Identity.Data;
using ChromeHeart.Data;
using ChromeHeart.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;

namespace ChromeHeart.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> RegisterConfirm(RegisterViewModels models, [FromServices] UserManager<ApplicationUser> userManager, [FromServices] RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(models.userName);
            if (user == null)
            {
                user = new ApplicationUser();
                user.UserName = models.userName;
                user.Email = models.userName;
                user.EmailConfirmed = true;
            }
            await userManager.CreateAsync(user, models.password);
			if (await userManager.IsInRoleAsync(user, "Customer") == false)
			{
				await userManager.AddToRoleAsync(user, "Customer");
			}
			return RedirectToAction("Login", "Home");
        }
        public async Task<IActionResult> LoginConfirm(LoginViewModels models, [FromServices] UserManager<ApplicationUser> userManager, [FromServices] SignInManager<ApplicationUser> signInManager)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(models.userName);
            if (user == null)
            {
                return RedirectToAction("Register", "Home");
            }
            else
            {
                var success = await signInManager.PasswordSignInAsync(user, models.password, false, false);
                if (success.Succeeded)
                {
					if (await userManager.IsInRoleAsync(user, "Admin") == true)
					{
						return RedirectToAction("Index", "Admin");
					}
                    else
                    {
                        
                    return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Register", "Home");
                }
            }
        }
        public async Task<IActionResult> Logout([FromServices] SignInManager<ApplicationUser> signInManager)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }



}
