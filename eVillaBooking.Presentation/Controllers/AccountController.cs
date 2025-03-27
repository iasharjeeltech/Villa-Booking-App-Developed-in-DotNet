using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Application.Utility;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Presentation.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVillaBooking.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IUnitOfWork unitOfWork,
               UserManager<ApplicationUser> userManager,
               RoleManager<IdentityRole> roleManager,
               SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null) 
        {
            returnUrl ?? = Url.Content("~/");
            // Ensure roles exist
            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Customer)).Wait();
            }

            // Create a new registerVM and populate the RoleList
            RegisterVM registerVM = new RegisterVM
            {
                RoleList = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }),
                RedirectURl = returnUrl
            };

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                registerVM.RoleList = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                });
                return View(registerVM);
            }

            ApplicationUser user = new ApplicationUser
            {
                Name = registerVM.Name,
                CreatedAt = DateTime.Now,
                Email = registerVM.Email,
                EmailConfirmed = true,
                NormalizedEmail = registerVM.Email.ToUpper(),
                PhoneNumber = registerVM.PhoneNumber,
                UserName = registerVM.Email
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded)
            {
                // Assign role to the user
                if (!string.IsNullOrEmpty(registerVM.Role))
                    await _userManager.AddToRoleAsync(user, registerVM.Role);
                else
                    await _userManager.AddToRoleAsync(user, StaticDetails.Role_Customer);

                // Sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Redirect to the specified URL or home page
                return string.IsNullOrEmpty(registerVM.RedirectURl) ?
                                  RedirectToAction("Index", "Home") :
                                  LocalRedirect(registerVM.RedirectURl);
            }

            // Add password validation errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            // Repopulate RoleList in case of errors
            registerVM.RoleList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            return View(registerVM);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            LoginVM loginVM = new LoginVM()
            {
                RedirectURL = returnUrl
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
             var result =  await _signInManager.PasswordSignInAsync(loginVM.Email,
                                                    loginVM.Password,
                                                   isPersistent: loginVM.RememberMe,
                                                   lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return string.IsNullOrEmpty(loginVM.RedirectURL) ?
                                    RedirectToAction("Index", "Home") :
                                    LocalRedirect(loginVM.RedirectURL); 
                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
             await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {

            return View();
        }
    }
}
