using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapitSiteAdmin.Areas.Identity.Data;
using MapitSiteAdmin.Models;

namespace MapitSiteAdmin.Controllers
{
    public class DiagramsController : Controller
    {
        private readonly mapitDbContext _context;

        public DiagramsController(mapitDbContext context)
        {
            _context = context;
        }

        // GET: Diagrams
        public async Task<IActionResult> Index()
        {
            var mapitDbContext = _context.Diagrams.Include(d => d.Project);
            return View(await mapitDbContext.ToListAsync());
        }

        // GET: Diagrams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diagrams == null)
            {
                return NotFound();
            }

            var diagram = await _context.Diagrams
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DiagramId == id);
            if (diagram == null)
            {
                return NotFound();
            }

            return View(diagram);
        }

        // GET: Diagrams/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name");
            return View();
        }

        // POST: Diagrams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiagramId,Name,Specification,Created,Updated,ProjectId,CategId,UserId")] Diagram diagram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", diagram.ProjectId);
            return View(diagram);
        }

        // GET: Diagrams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diagrams == null)
            {
                return NotFound();
            }

            var diagram = await _context.Diagrams.FindAsync(id);
            if (diagram == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", diagram.ProjectId);
            return View(diagram);
        }

        // POST: Diagrams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiagramId,Name,Specification,Created,Updated,ProjectId,CategId,UserId")] Diagram diagram)
        {
            if (id != diagram.DiagramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagramExists(diagram.DiagramId))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", diagram.ProjectId);
            return View(diagram);
        }

        // GET: Diagrams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diagrams == null)
            {
                return NotFound();
            }

            var diagram = await _context.Diagrams
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DiagramId == id);
            if (diagram == null)
            {
                return NotFound();
            }

            return View(diagram);
        }

        // POST: Diagrams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diagrams == null)
            {
                return Problem("Entity set 'mapitDbContext.Diagrams'  is null.");
            }
            var diagram = await _context.Diagrams.FindAsync(id);
            if (diagram != null)
            {
                _context.Diagrams.Remove(diagram);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagramExists(int id)
        {
          return _context.Diagrams.Any(e => e.DiagramId == id);
        }
    }
}
