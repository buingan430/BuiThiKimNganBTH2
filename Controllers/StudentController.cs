using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuiThiKimNganBTH2.Data;
using BuiThiKimNganBTH2.Models;

namespace BuiThiKimNganBTH2.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Students.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Create (Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }
    }
}