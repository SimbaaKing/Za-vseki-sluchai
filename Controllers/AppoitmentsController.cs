using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManicureAndPedicureSalon.Data;
using ManicureAndPedicureSalon.Models;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "User, Admin")]
        public IActionResult Create()
        {
            AppoitmentsVM model = new AppoitmentsVM();

            model.Services = _context.Services.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ServiceId.ToString(),
                Selected = (x.ServiceId == model.ServiceId)
            }).ToList();

            return View(model);
        }

        // POST: Appoitments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ServiceId,DateVisit,TimeVisit,Date")] AppoitmentsVM appoitment)
        {
            appoitment.Date = DateTime.Now;
            if (!ModelState.IsValid)
            { 
                AppoitmentsVM model = new AppoitmentsVM();
                model.Services = _context.Services.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ServiceId.ToString(),
                    Selected = (x.ServiceId == model.ServiceId)
                }
                ).ToList();
                return View(model);
            }

            Appoitment ModelToDB = new Appoitment();
            {
                ModelToDB.ServiceId = appoitment.ServiceId;
                //ClientId = _userManager.GetUserId(User),
                ModelToDB.Date = DateTime.Now;
                ModelToDB.DateVisit = DateTime.Now;
                ModelToDB.TimeVisit = DateTime.UtcNow;
            }; 
                _context.Add(ModelToDB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
            AppoitmentsVM model = new AppoitmentsVM();
            model.Services = _context.Services.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ServiceId.ToString(),
                Selected = (x.ServiceId == model.ServiceId)
            }
            ).ToList();
            return View(model);
        }

        // POST: Appoitments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ServiceId,DateVisit,TimeVisit,Date")] AppoitmentsVM appoitment)
        {
            appoitment.Date = DateTime.Now;
            if (id != appoitment.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(appoitment);
            }

            AppoitmentsVM ModelFromDB = new ()
            {
                ServiceId = appoitment.ServiceId,
                //ClientId = _userManager.GetUserId(User),
                Date = DateTime.Now
            };

            try
                {
                    _context.Update(ModelFromDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppoitmentExists(ModelFromDB.Id))
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
