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
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Services.Include(s => s.Employer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Employer)
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create

        [Authorize(Roles = "User, Admin")]

        public IActionResult Create()
        {
            ServicesVM model = new ServicesVM();

            model.Employers = _context.Employers.Select(x => new SelectListItem
            {
                Value = x.EmployerId.ToString(),
                Text = x.Name,
                Selected = x.EmployerId == model.EmployerId
            }).ToList();

            return View(model);
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,Name,Category,Description,EmployerId,Images,Price,DateRegister")] ServicesVM service)
        {
            service.DateRegister = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ServicesVM model = new ServicesVM();
                model.Employers = _context.Employers.Select(x => new SelectListItem
                {
                    Value = x.EmployerId.ToString(),
                    Text = x.Name,
                    Selected = x.EmployerId == model.EmployerId
                }).ToList();
                return View(model);
            }
            
                Service modelToDb = new Service();
            {
                modelToDb.ServiceId = service.ServiceId;
                modelToDb.Name = service.Name;
                modelToDb.Price = service.Price;
                modelToDb.Images = service.Images;
                modelToDb.DateRegister = service.DateRegister;
                modelToDb.Category = service.Category;
                modelToDb.Description = service.Description;
                modelToDb.EmployerId = service.EmployerId;
            }

                _context.Services.Add(modelToDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ServicesVM model = new ServicesVM();
            model.EmployerId = service.EmployerId;
            model.Name = service.Name;
            

            model.Employers = _context.Employers.Select(x => new SelectListItem
            {
                Value = x.EmployerId.ToString(),
                Text = x.Name,
                Selected = x.EmployerId == model.EmployerId
            }).ToList();

            return View(model);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,Name,Category,Description,EmployerId,Images,Price,DateRegister")] ServicesVM service)
        {
            service.DateRegister = DateTime.Now;
            //Service modelToDb = await _context.Services.FindAsync(Service);
            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(service);
            }
            Service modelFromDb = new ();
            {
                //modelToDb.ServiceId = service.ServiceId;
                modelFromDb.Name = service.Name;
                modelFromDb.Price = service.Price;
                modelFromDb.Images = service.Images;
                modelFromDb.DateRegister = service.DateRegister;
                modelFromDb.Category = service.Category;
                modelFromDb.Description = service.Description;
                modelFromDb.EmployerId = service.EmployerId;
            };
            try
            {
                _context.Update(modelFromDb);
                await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                  if (!ServiceExists(modelFromDb.ServiceId))
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

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Employer)
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceId == id);
        }
    }
}
