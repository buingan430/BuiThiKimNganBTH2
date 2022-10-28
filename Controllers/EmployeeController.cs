using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuiThiKimNganBTH2.Data;
using BuiThiKimNganBTH2.Models;

namespace BuiThiKimNganBTH2.Controllers
{
   public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.Employees.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (Employee std)
        {
            if(ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
            
        }
        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var Employee = await _context.Employees.FindAsync(id);
            if (Employee == null)
            {
               // return NotFound();
               return View("NotFound");
            }
            return View(Employee);
        }
        // POST: Employee/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult>Edit(string id, [Bind("EmployeeID,EmployeeName")] Employee std)
        {
            if (id != std.EmployeeID)
            {
               // return NotFound();
               return View("NotFound");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(std);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if( !EmployeeExists(std.EmployeeID))
                    {
                       // return NotFound();
                       return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
               // return NotFound();
               return View("NotFound");
            }
            var std = await _context.Employees
                .FirstOrDefaultAsync(m =>m.EmployeeID ==id);
            if (std == null)
            {
               // return NotFound();
               return View("NotFound");
            }
            return View(std);
        }
        // POST: Product/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e =>e.EmployeeID ==id);
        }
    }
}
    