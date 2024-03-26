using MapitSiteAdmin.Areas.Identity.Data;
using MapitSiteAdmin.Seeds;
using MapitSiteAdmin.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace MapitSiteAdmin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UserRolesController : Controller
    {
        private readonly mapitDbContext _context;
        private readonly UserManager<MapitUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<MapitUser> _signInManager;

        public UserRolesController(mapitDbContext context,
            UserManager<MapitUser> userManage,
            RoleManager<IdentityRole> roleManage,
            SignInManager<MapitUser> signInManager)
        {
            _context = context;
            _userManager = userManage; //context User
            _roleManager = roleManage; // context Roles
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index(string userId)
        {

            var viewModel = new List<UserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
           
            //_context.Roles.ToList()
            var x = userId;
            var y = user;
            foreach(var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleName = role.Name,
                };
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            var model = new ManageUserRolesViewModel()
            {
                UserName = user.UserName,
                UserId = userId,
                UserRoles = viewModel
            };
            return View(model);
        }

        //Update Role
        public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
        {
            
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(ur => ur.Selected).Select(ur => ur.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            //await _signInManager.RefreshSignInAsync(currentUser);
            //await DefaultUsers.SeedSuperAdminAsync(_userManager, _roleManager);
            TempData["success"] = "User Roles changed successfuly";
            //return RedirectToAction("Index", new { userId = id });
            return RedirectToAction("Index", "Users");
        }
    }
}
