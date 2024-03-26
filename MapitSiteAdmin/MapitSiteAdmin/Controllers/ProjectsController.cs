using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapitSiteAdmin.Areas.Identity.Data;
using MapitSiteAdmin.Models;
using MapitSiteAdmin.Constants.Users;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

namespace MapitSiteAdmin.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly mapitDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public ProjectsController(mapitDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        // GET: Projects
        [Authorize(Policy = "Permissions.Products.View")]
        public async Task<IActionResult> Index()
        {
                var mapitDbContext = _context.Projects.Include(p => p.MapitUser);
            return View(await mapitDbContext.ToListAsync());
        }


        // GET: Projects/Details/5
        [Authorize(Policy = "Permissions.Products.View")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.MapitUser)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Policy = "Permissions.Products.Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("ProjectId,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(project);
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", project.UserId);
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Policy = "Permissions.Products.Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", project.UserId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Name,Created,Updated,UserId")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", project.UserId);
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Policy = "Permissions.Products.Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.MapitUser)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'mapitDbContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
