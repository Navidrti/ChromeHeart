using ChromeHeart.Areas.Identity.Data;
using ChromeHeart.Data;
using ChromeHeart.Models;
using ChromeHeart.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChromeHeart.Controllers
{
	[Authorize(Policy =("AdminPolicy"))]
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult ListOfUsers([FromServices] DbWebFinal db, [FromServices] UserManager<ApplicationUser> userManager, [FromServices] RoleManager<IdentityRole> roleManager)
		{
			var users = db.Users.ToList();
			var userroles = db.UserRoles.ToList();
			var roles = db.Roles.ToList();
			List<UsersViewModels> lstusers = new List<UsersViewModels>();
			users.ForEach(x =>
			{
				var userrole = userroles.Where(y => y.UserId == x.Id).ToList();
				userrole.ForEach(k =>
				{
					var rolename = roles.Where(h => h.Id == k.RoleId).ToList();
					rolename.ForEach(m =>
					{
						UsersViewModels usersView = new UsersViewModels();
						usersView.username = x.UserName;
						usersView.roleName = m.Name;
						lstusers.Add(usersView);
					});
				});
			});
			return View(lstusers);
		}
    }
}
