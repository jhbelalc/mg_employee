using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mg_employee.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string contractTypeName { get; set; }
        public int roleId { get; set; }
        public string roleName { get; set; }
        public object roleDescription { get; set; }
        public decimal hourlySalary { get; set; }
        public decimal monthlySalary { get; set; }
        public decimal anualSalary { get; set; }
    }
}