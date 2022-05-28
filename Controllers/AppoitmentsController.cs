using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManicureAndPedicureSalon.Data;

namespace ManicureAndPedicureSalon.Controllers
{
    public class AppoitmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppoitmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appoitments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Appoitments.Include(a => a.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appoitments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appoitment = await _context.Appoitments
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appoitment == null)
            {
                return NotFound();
            }
            

            return View(appoitment);
        }

        // GET: Appoitments/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId");
            return View();
        }

        // POST: Appoitments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ServiceId,DateVisit,TimeVisit,Date")] Appoitment appoitment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appoitment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", appoitment.ServiceId);
            return View(appoitment);
        }

        // GET: Appoitments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appoitment = await _context.Appoitments.FindAsync(id);
            if (appoitment == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", appoitment.ServiceId);
            return View(appoitment);
        }

        // POST: Appoitments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ServiceId,DateVisit,TimeVisit,Date")] Appoitment appoitment)
        {
            if (id != appoitment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appoitment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppoitmentExists(appoitment.Id))
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
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", appoitment.ServiceId);
            return View(appoitment);
        }

        // GET: Appoitments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appoitment = await _context.Appoitments
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appoitment == null)
            {
                return NotFound();
            }

            return View(appoitment);
        }

        // POST: Appoitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appoitment = await _context.Appoitments.FindAsync(id);
            _context.Appoitments.Remove(appoitment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppoitmentExists(int id)
        {
            return _context.Appoitments.Any(e => e.Id == id);
        }
    }
}
