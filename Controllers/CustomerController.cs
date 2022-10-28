using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuiThiKimNganBTH2.Data;
using BuiThiKimNganBTH2.Models;

namespace BuiThiKimNganBTH2.Controllers
{
   public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.Customers.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (Customer std)
        {
            if(ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
            
        }
        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var Customer = await _context.Customers.FindAsync(id);
            if (Customer== null)
            {
               // return NotFound();
               return View("NotFound");
            }
            return View(Customer);
        }
        // POST: Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult>Edit(string id, [Bind("CustomerID,CustomerName")] Customer std)
        {
            if (id != std.CustomerID)
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
                    if( !CustomerExists(std.CustomerID))
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
            var std = await _context.Customers
                .FirstOrDefaultAsync(m =>m.CustomerID ==id);
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
            var std = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e =>e.CustomerID ==id);
        }
    }
}
    