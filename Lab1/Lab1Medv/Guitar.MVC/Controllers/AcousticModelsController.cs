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
    public class AcousticModelsController : Controller
    {
        private readonly GuitarContext _context;

        public AcousticModelsController(GuitarContext context)
        {
            _context = context;
        }

        // GET: AcousticModels
        public async Task<IActionResult> Index()
        {
            var guitarContext = _context.Acoustics.Include(a => a.Player);
            return View(await guitarContext.ToListAsync());
        }

        // GET: AcousticModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acousticModel = await _context.Acoustics
                .Include(a => a.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acousticModel == null)
            {
                return NotFound();
            }

            return View(acousticModel);
        }

        // GET: AcousticModels/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name");
            return View();
        }

        // POST: AcousticModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HasPiezo,StringType,Id,Name,StringCount,ScaleLength,Price,PlayerId")] AcousticModel acousticModel)
        {
            if (ModelState.IsValid)
            {
                acousticModel.Id = Guid.NewGuid();
                _context.Add(acousticModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", acousticModel.PlayerId);
            return View(acousticModel);
        }

        // GET: AcousticModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acousticModel = await _context.Acoustics.FindAsync(id);
            if (acousticModel == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", acousticModel.PlayerId);
            return View(acousticModel);
        }

        // POST: AcousticModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("HasPiezo,StringType,Id,Name,StringCount,ScaleLength,Price,PlayerId")] AcousticModel acousticModel)
        {
            if (id != acousticModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acousticModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcousticModelExists(acousticModel.Id))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", acousticModel.PlayerId);
            return View(acousticModel);
        }

        // GET: AcousticModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acousticModel = await _context.Acoustics
                .Include(a => a.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acousticModel == null)
            {
                return NotFound();
            }

            return View(acousticModel);
        }

        // POST: AcousticModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var acousticModel = await _context.Acoustics.FindAsync(id);
            if (acousticModel != null)
            {
                _context.Acoustics.Remove(acousticModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcousticModelExists(Guid id)
        {
            return _context.Acoustics.Any(e => e.Id == id);
        }
    }
}
