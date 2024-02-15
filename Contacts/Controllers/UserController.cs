using Contacts.Data.Models;
using Contacts.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
	public class UserController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUserStore<ApplicationUser> _userStore;

		public UserController(
			SignInManager<ApplicationUser> signInManager,
			IUserStore<ApplicationUser> userStore,
			UserManager<ApplicationUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_userStore = userStore;
		}


		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			ApplicationUser user = new()
			{
				UserName = model.UserName,
				Email = model.Email,
				Password = model.Password
			};

			await _userStore.SetUserNameAsync(user, model.UserName, CancellationToken.None);
			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				await _signInManager.PasswordSignInAsync(
					model.UserName,
					model.Password,
					true,
					false);

				return RedirectToAction("Index", "Home");
			}

			return View();
		}
	}
}
