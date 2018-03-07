using System.Net.Http;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using mg_employee.Models;
using System;

namespace mg_employee.Controllers
{
    public interface IFactory
    {
        void calcAnualSalary(Employee employee);
    }

    public class hourSalary : IFactory
    {
        public void calcAnualSalary(Employee employee)
        {
            throw new NotImplementedException();
        }
    }

    public class monthSalary : IFactory
    {
        public void calcAnualSalary(Employee employee)
        {
            throw new NotImplementedException();
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync(int ID=0)
        {
            //http://masglobaltestapi.azurewebsites.net/api/Employees
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://masglobaltestapi.azurewebsites.net/api/Employees");
            var employeeList = JsonConvert.DeserializeObject<List<Employee>>(json);
            var filtList = new Employee();
            if (ID > 0)
            {
                for (int i = 0; i < employeeList.Count; i++)
                {
                    if (employeeList[i].id == ID)
                    {
                        filtList.id = ID;
                        filtList.name = employeeList[i].name;
                        filtList.contractTypeName = employeeList[i].contractTypeName;
                        filtList.roleId = employeeList[i].roleId;
                        filtList.roleName = employeeList[i].roleName;
                        filtList.roleDescription = employeeList[i].roleDescription;
                        filtList.hourlySalary = employeeList[i].hourlySalary;
                        filtList.monthlySalary = employeeList[i].monthlySalary;
                        
                        switch (employeeList[i].contractTypeName)
                        {
                            case "HourlySalaryEmployee":
                                filtList.anualSalary = 120 * filtList.hourlySalary * 12;
                                break;
                            case "MonthlySalaryEmployee":
                                filtList.anualSalary = filtList.monthlySalary * 12;
                                break;
                            default:
                                break;
                        }
                        if (employeeList[i].contractTypeName== "")
                        {
                            
                        }
                    }
                }
                return View("edEmployee", filtList);
            }
            foreach (var employee in employeeList)
            {
                switch (employee.contractTypeName)
                {
                    case "HourlySalaryEmployee":
                        employee.anualSalary = 120 * employee.hourlySalary * 12;
                        break;
                    case "MonthlySalaryEmployee":
                        employee.anualSalary = employee.monthlySalary * 12;
                        break;
                    default:
                        break;
                }
            }
            return View(employeeList);
        }



        public ActionResult About()
        {
            ViewBag.Message = "This is a MVC test getting data from Webapi.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Here you can contact me:";

            return View();
        }
        

    }
}