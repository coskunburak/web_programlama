using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly UserManager<IdentityUser> _userManager;
	public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		
	}

	[HttpGet]
	public IActionResult Login() => View();

	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (!ModelState.IsValid) return View(model);

		var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
		if (result.Succeeded) return RedirectToAction("Index", "Home");

		ModelState.AddModelError(string.Empty, "Giriþ baþarýsýz. Bilgilerinizi kontrol edin.");
		return View(model);
	}

	[HttpGet]
	public IActionResult Register() => View();

	[HttpPost]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (!ModelState.IsValid) return View(model);

		var user = new IdentityUser { UserName = model.Email, Email = model.Email };
		var result = await _userManager.CreateAsync(user, model.Password);
		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, "User");
			await _signInManager.SignInAsync(user, isPersistent: false);
			return RedirectToAction("Index", "Home");
		}

		foreach (var error in result.Errors)
			ModelState.AddModelError(string.Empty, error.Description);

		return View(model);
	}

	[HttpGet]
	public async Task<IActionResult> Logout()
	{
		if (User.Identity.IsAuthenticated)
		{
			await _signInManager.SignOutAsync();
		}
		return RedirectToAction("Index", "Home");
	}
}
