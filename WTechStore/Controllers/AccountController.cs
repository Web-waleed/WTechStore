using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WTechStore.Models.ViewModels;

namespace WTechStore.Controllers
{
    public class AccountController : Controller
    {
        #region Configuration
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        #endregion

        #region USer
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email

                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid User or Password");
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult UserStatistics()
        {
            ViewBag.TotalUsers = userManager.Users.Count();  // Get the total number of users
            return View();  // Update this to redirect to the appropriate view
        }
        #endregion

        #region Role
        [HttpGet]
        public async Task<IActionResult> RolesList()
        {
            return View(await roleManager.Roles.ToListAsync());
        }
        [HttpGet]
        public IActionResult CreateRoles()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoles(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };
              var result= await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));   
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditRoles(String id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Role = await roleManager.FindByIdAsync(id);
            if (Role == null) { return NotFound(); }
            EditRoleViewModel model = new EditRoleViewModel
            {
              RoleName=Role.Name!,
                RoleId = Role.Id
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> editRoles(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var x = await roleManager.FindByIdAsync(model.RoleId);
                if (x == null) { return NotFound(); }
                x.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(x);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                foreach (var Err in result.Errors)
                {
                    ModelState.AddModelError(Err.Code, Err.Description);
                }
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            DeleteRoleViewModel model = new DeleteRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name!
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                else
                {
                    ModelState.AddModelError("", "Error deleting role");
                }
            }
            return View(model);
        }
        #endregion

    }
}
