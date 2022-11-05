using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BuiThiKimNganBTH2.Models
{
    public class Employee
    {
        [Key]
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string Adress { get; set; }
    }
}