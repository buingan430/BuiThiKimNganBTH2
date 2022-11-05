using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuiThiKimNganBTH2.Data;
using BuiThiKimNganBTH2.Models.Process;
using BuiThiKimNganBTH2.Models;


namespace BuiThiKimNganBTH2.Controllers
{
   public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        public PersonController (ApplicationDbContext context)
        { 
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persons.ToListAsync());
        }
        public bool PersonExists(string id)
        {
            return _context.Persons.Any(e =>e.PersonID == id);
        }

        // public async Task<IActionResult> Index()
        // {
        //     var model = await _context.Persons.ToListAsync();
        //     return View(model);
        // }
        // public IActionResult Create()
        // {
        //     return View();
        // }
        // [HttpPost]
        // public async Task<IActionResult> Create (Person std)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         _context.Add(std);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(std);
            
        // }
        // // GET: Person/Edit/5
        // public async Task<IActionResult> Edit(string id)
        // {
        //     if (id == null)
        //     {
        //         return View("NotFound");
        //     }
        //     var Person = await _context.Persons.FindAsync(id);
        //     if (Person== null)
        //     {
        //        // return NotFound();
        //        return View("NotFound");
        //     }
        //     return View(Person);
        // }
        // // POST: Person/Edit
        // [HttpPost]
        // [ValidateAntiForgeryToken]

        // public async Task<IActionResult>Edit(string id, [Bind("PersonID,PersonName")] Person std)
        // {
        //     if (id != std.PersonID)
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
        //             if( !PersonExists(std.PersonID))
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
        //     var std = await _context.Persons
        //         .FirstOrDefaultAsync(m =>m.PersonID ==id);
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
        //     var std = await _context.Persons.FindAsync(id);
        //     _context.Persons.Remove(std);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }
        // private bool PersonExists(string id)
        // {
        //     return _context.Persons.Any(e =>e.PersonID ==id);
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
                            var emp = new Person();
                            emp.PersonID = dt.Rows[i][0].ToString ();
                            emp.PersonName = dt.Rows[i][1].ToString ();
                            emp.Adress = dt.Rows[i][2].ToString ();
                            _context.Persons.Add(emp);
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
    