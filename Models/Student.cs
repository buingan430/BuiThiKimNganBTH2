using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BuiThiKimNganBTH2.Models
{
    public class Student
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public string Adress { get; set; }
        public string FacultyID {get; set;}
        [ForeignKey("FacultyID")]
        public Faculty? Faculty {get; set;}
    }
}