using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Guitar.Infrastructure;
using Guitar.Infrastructure.Models;

namespace Guitar.MVC.Controllers
{
    public class GuitarModelsController : Controller
    {
        private readonly GuitarContext _context;

        public GuitarModelsController(GuitarContext context)
        {
            _context = context;
        }

        // GET: GuitarModels
        public async Task<IActionResult> Index()
        {
            var guitarContext = _context.Guitars.Include(g => g.Player);
            return View(await guitarContext.ToListAsync());
        }

        // GET: GuitarModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guitarModel = await _context.Guitars
                .Include(g => g.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guitarModel == null)
            {
                return NotFound();
            }

            return View(guitarModel);
        }

        // GET: GuitarModels/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name");
            return View();
        }

        // POST: GuitarModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StringCount,ScaleLength,Price,PlayerId")] GuitarModel guitarModel)
        {
            if (ModelState.IsValid)
            {
                guitarModel.Id = Guid.NewGuid();
                _context.Add(guitarModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", guitarModel.PlayerId);
            return View(guitarModel);
        }

        // GET: GuitarModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guitarModel = await _context.Guitars.FindAsync(id);
            if (guitarModel == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", guitarModel.PlayerId);
            return View(guitarModel);
        }

        // POST: GuitarModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,StringCount,ScaleLength,Price,PlayerId")] GuitarModel guitarModel)
        {
            if (id != guitarModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guitarModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuitarModelExists(guitarModel.Id))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", guitarModel.PlayerId);
            return View(guitarModel);
        }

        // GET: GuitarModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guitarModel = await _context.Guitars
                .Include(g => g.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guitarModel == null)
            {
                return NotFound();
            }

            return View(guitarModel);
        }

        // POST: GuitarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var guitarModel = await _context.Guitars.FindAsync(id);
            if (guitarModel != null)
            {
                _context.Guitars.Remove(guitarModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuitarModelExists(Guid id)
        {
            return _context.Guitars.Any(e => e.Id == id);
        }
    }
}
