using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuiThiKimNganBTH2.Data;
using BuiThiKimNganBTH2.Models.Process;
using BuiThiKimNganBTH2.Models;
using BuiThiKimNganBTH2.Models.Process;


namespace BuiThiKimNganBTH2.Controllers
{
   public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private StringProcess StrPro = new StringProcess();
        private ExcelProcess _excelProcess = new ExcelProcess();
        public EmployeeController (ApplicationDbContext context)
        { 
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Employees.ToListAsync());
        }
        // public bool EmployeeExists(string id)
        // {
        //     return _context.Employees.Any(e =>e.EmpID == id);
        // }

        // public async Task<IActionResult> Index()
        // {
        //     var model = await _context.Employees.ToListAsync();
        //     return View(model);
        // }
        public IActionResult Create()
        {
            var id = _context.Employees.OrderByDescending(m =>m.EmpName).First ().EmpID;
            ViewBag.newKey = StrPro.AutoGenerateCode(id);
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
        // public async Task<IActionResult> Edit(string id)
        // {
        //     if (id == null)
        //     {
        //         return View("NotFound");
        //     }
        //     var Employee = await _context.Employees.FindAsync(id);
        //     if (Employee == null)
        //     {
        //        // return NotFound();
        //        return View("NotFound");
        //     }
        //     return View(Employee);
        // }
        // // POST: Employee/Edit
        // [HttpPost]
        // [ValidateAntiForgeryToken]

        // public async Task<IActionResult>Edit(string id, [Bind("EmployeeID,EmployeeName")] Employee std)
        // {
        //     if (id != std.EmployeeID)
        //     {
        //        // return NotFound();
        //        return View("NotFound");
        //     }
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(std);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if( !EmployeeExists(std.EmployeeID))
        //             {
        //                // return NotFound();
        //                return View("NotFound");
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(std);
        // }
        // public async Task<IActionResult> Delete(string id)
        // {
        //     if (id == null)
        //     {
        //        // return NotFound();
        //        return View("NotFound");
        //     }
        //     var std = await _context.Employees
        //         .FirstOrDefaultAsync(m =>m.EmployeeID ==id);
        //     if (std == null)
        //     {
        //        // return NotFound();
        //        return View("NotFound");
        //     }
        //     return View(std);
        // }
        // // POST: Product/Delete/
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(string id)
        // {
        //     var std = await _context.Employees.FindAsync(id);
        //     _context.Employees.Remove(std);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }
        // private bool EmployeeExists(string id)
        // {
        //     return _context.Employees.Any(e =>e.EmployeeID ==id);
        // }

        public async Task<IActionResult>Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upload(IFormFile file)
        {
            if( file !=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension !=".xls" && fileExtension !=".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    var fileName  = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        for (int i=0; i< dt.Rows.Count; i++)
                        {
                            var emp = new Employee();
                            emp.EmpID = dt.Rows[i][0].ToString ();
                            emp.EmpName = dt.Rows[i][1].ToString ();
                            emp.Adress = dt.Rows[i][2].ToString ();
                            _context.Employees.Add(emp);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
    }
}
    