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
    public class ElectricModelsController : Controller
    {
        private readonly GuitarContext _context;

        public ElectricModelsController(GuitarContext context)
        {
            _context = context;
        }

        // GET: ElectricModels
        public async Task<IActionResult> Index()
        {
            var guitarContext = _context.Electrics.Include(e => e.Player);
            return View(await guitarContext.ToListAsync());
        }

        // GET: ElectricModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electricModel = await _context.Electrics
                .Include(e => e.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electricModel == null)
            {
                return NotFound();
            }

            return View(electricModel);
        }

        // GET: ElectricModels/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name");
            return View();
        }

        // POST: ElectricModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PickupCount,VibratoSystem,Id,Name,StringCount,ScaleLength,Price,PlayerId")] ElectricModel electricModel)
        {
            if (ModelState.IsValid)
            {
                electricModel.Id = Guid.NewGuid();
                _context.Add(electricModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", electricModel.PlayerId);
            return View(electricModel);
        }

        // GET: ElectricModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electricModel = await _context.Electrics.FindAsync(id);
            if (electricModel == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", electricModel.PlayerId);
            return View(electricModel);
        }

        // POST: ElectricModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PickupCount,VibratoSystem,Id,Name,StringCount,ScaleLength,Price,PlayerId")] ElectricModel electricModel)
        {
            if (id != electricModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electricModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectricModelExists(electricModel.Id))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", electricModel.PlayerId);
            return View(electricModel);
        }

        // GET: ElectricModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electricModel = await _context.Electrics
                .Include(e => e.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electricModel == null)
            {
                return NotFound();
            }

            return View(electricModel);
        }

        // POST: ElectricModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var electricModel = await _context.Electrics.FindAsync(id);
            if (electricModel != null)
            {
                _context.Electrics.Remove(electricModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectricModelExists(Guid id)
        {
            return _context.Electrics.Any(e => e.Id == id);
        }
    }
}
