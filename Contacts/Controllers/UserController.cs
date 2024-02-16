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

			foreach (var err in result.Errors)
			{
				ModelState.AddModelError("", err.Description);
			}

			return View(model);
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _signInManager.PasswordSignInAsync(
					model.UserName,
					model.Password,
					true,
					false);

			if (!result.Succeeded)
			{
				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				return View(model);
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}
