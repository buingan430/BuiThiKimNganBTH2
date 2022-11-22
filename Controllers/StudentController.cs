using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuiThiKimNganBTH2.Data;
using BuiThiKimNganBTH2.Models.Process;
using BuiThiKimNganBTH2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuiThiKimNganBTH2.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
       
        private StringProcess strPro = new StringProcess();
        public StudentController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View ( await _context.Students.ToListAsync());
          
        }
       
         public IActionResult Create()
        {
            var newStudentID = "STD001";
            var countStudent = _context.Students.Count();
            if(countStudent>0)
            {
                var studentID = _context.Students.OrderByDescending(m =>m.StudentID).First().StudentID;
                // sinh ma tu dong
                newStudentID = strPro.AutoGenerateCode(studentID);
            }
            ViewBag.newID = newStudentID;
            ViewData["FacultyID"]=new SelectList (_context.Faculty, "FacultyID", "FacultyName");
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,StudentName,Adress,FacultyID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FacultyID"] =new SelectList(_context.Faculty, "FacultyID", "FacultyName", student.FacultyID);
            return View(student);
        }
         public async Task<IActionResult>Upload()
        {
            return View();
        }
        // [HttpPost]
        // [ValidateAntiForgeryToken]
       
        public async Task<IActionResult>Upload(IFormFile file)
        {
            if (file!=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension !=".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    //rename
                    var fileName = DateTime.Now.ToShortTimeString() +fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath,FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                    
                        // read data from
                        //var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop to read data from dt
                        //for (int i=0; i< dt.Rows.Count;i++)
                        {
                            //create a new Employee object
                            var std = new Student();
                            // set values for attrinutes
                            // std.StudentID= dt.Rows[i][0].ToString();
                            // std.StudentName= dt.Rows[i][1].ToString();
                            // std.Adress= dt.Rows[i][2].ToString();
                             //add object to Context
                             _context.Students.Add(std);
                        }
                        //save to database 
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
    
            return View();
        }
        private bool StudentExists(string id)
         {
           return _context.Students.Any(e =>e.StudentID ==id);
        }
    }

}


    