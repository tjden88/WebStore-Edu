using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;

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

        [Authorize]
        public IActionResult Index() => View();


        [Authorize]
        public async Task<IActionResult> Orders([FromServices] IOrderService OrderService, [FromServices] IMapper Mapper)
        {
            var userOrders = await OrderService.GetUserOrdersAsync(await _UserManager.FindByNameAsync(User.Identity!.Name));
            return View(Mapper.Map<IEnumerable<UserOrderViewModel>>(userOrders));
        }


        public IActionResult Authorize() => View();
        public IActionResult Login(string ReturnUrl) => View(new LoginUserViewModel(){ReturnUrl = ReturnUrl});
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
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
                await _UserManager.AddToRoleAsync(user, Role.Users);
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

        [HttpPost, ValidateAntiForgeryToken]
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
                return LocalRedirect(Model.ReturnUrl ?? "/");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");

            return View(Model);
        }

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
