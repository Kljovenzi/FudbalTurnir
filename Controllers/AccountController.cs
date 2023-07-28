using FluentValidation;
using FluentValidation.Results;
using FudbalskiTurnir_FilipNikolic.Models.Validation.AccountValidators;
using FudbalskiTurnir_FilipNikolic.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FudbalskiTurnir_FilipNikolic.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// UserManager  :         SignInManager
        /// CreateAsync            SignInAsync
        /// DeleteAsync            SignOutAsync
        /// UpdateAsync            IsSignedIn
        /// </summary>
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogInAsync(LogInViewModel model)
        {
            var validator = new LogInViewModelValidator();
            ValidationResult result = validator.Validate(model);
            if (result.IsValid)
            {
                var signInResult = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if(signInResult.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }

                ModelState.AddModelError(String.Empty, "Invalid LogIn attempt");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var validator = new RegisterViewModelValidator();
            ValidationResult result = validator.Validate(model);
            if(result.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var createResult = await userManager.CreateAsync(user, model.Password);
                if (createResult.Succeeded)
                {
                    /*
                     * Prvi put kad se loguje, moramo mu dati admin prava da bi mogao da koristi aplikaciju
                     * proveravamo da li ima usera, tj. da li je nakon kreiranja usera, broj usera ==1
                     * ako jeste, dati mu admin role.
                     * Ovo je čisto da se može koristiti i prvi put, bez da se napravi pre ovoga user
                     */
                    if (userManager.Users.Count() == 1)
                    {
                        // Ako već postoji Admin role, onda je neće napraviti 
                        await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await roleManager.CreateAsync(new IdentityRole { Name = "User" });
                        await userManager.AddToRoleAsync(user, "User");
                    }
                   await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "Home");
                }
                foreach(var error in createResult.Errors)
                {
                    ModelState.AddModelError(" ", error.Description);
                }
            }
            return View(model);
        }
    }
}
