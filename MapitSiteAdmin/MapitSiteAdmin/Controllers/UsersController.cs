using MapitSiteAdmin.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace MapitSiteAdmin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : Controller
    {
        private readonly mapitDbContext _context;
        private readonly UserManager<MapitUser> _userManager;

        public UsersController(mapitDbContext mapitDbContext, UserManager<MapitUser> userManager)
        {
            _context = mapitDbContext;
            _userManager = userManager;

        }
        // GET: UsersController
        public async Task<ActionResult> IndexAsync()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);
            var roles = await _userManager.GetRolesAsync(user);
           
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //var t = await _userManager.Users.Include(user => user.)
            // Resolve the user via their email
            //var user = await _userManager.FindByEmailAsync();
            // Get the roles for the user
            ViewBag.Roles= await _userManager.GetRolesAsync(currentUser);

            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            return View(allUsersExceptCurrentUser);
        }

        // GET: UsersController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return View(user);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
