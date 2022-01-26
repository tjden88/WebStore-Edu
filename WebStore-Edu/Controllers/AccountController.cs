using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        public IActionResult Authorize() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            var user = new User()
            {
                UserName = Model.UserName
            };

            var registrationResult = await _UserManager.CreateAsync(user, Model.Password);
            if (registrationResult.Succeeded)
            {
                await _SignInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registrationResult.Errors)
            {
                switch (error.Code)
                {
                    case "PasswordTooShort":
                        ModelState.AddModelError("", "Пароль слишком короткий");
                        break;
                    case "PasswordRequiresUniqueChars":
                        ModelState.AddModelError("", "Пароль дложен содержать больше разных символов");
                        break;
                    case "DuplicateUserName":
                        ModelState.AddModelError("", "Такой пользователь уже зарегистрирован");
                        break;

                    default:
                        ModelState.AddModelError("", error.Description);
                        break;

                }
            }
            return View(Model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);


            var loginResult = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.Remember,
                false);

            if (loginResult.Succeeded)
            {
                return LocalRedirect(Model.RedirectUrl ?? "/");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");

            return View(Model);
        }

        public IActionResult Logout() => RedirectToAction("Index", "Home");

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
