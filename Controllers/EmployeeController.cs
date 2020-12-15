using EmployeePayrollMVC.Models;
using EmployeePayrollMVC.Models.Common;
using EmployeePayrollMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeePayrollMVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        // GET: Employee
        public ActionResult Index()
        {
            List<EmployeeDetailsModel> list = employeeRepository.GetEmployees();
            return View(list);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterEmployee(RegisterRequestModel employee)
        {
            bool result = false;
            if(ModelState.IsValid)
            {
                result = employeeRepository.RegisterEmployee(employee);
            }
            //ModelState.Clear();
            if(result==true)
            {
                return RedirectToAction("index");
            }
            return View("Register",employee);
        }
        [HttpGet]
        public ActionResult Edit(Employee model)
        {
            Employee emp = employeeRepository.GetEmployee(model.EmpId);
            return View(emp);
        }
        [HttpGet]
        public ActionResult Edit1(Employee model)
        {
            EmployeeDetailsModel emp = employeeRepository.GetEmployee1(model.EmpId);
            return View(emp);
        }
        [HttpPost]
        public ActionResult EditEmployee(Employee model)
        {
            int data = employeeRepository.Update(model);
            if (data != 0)
                return RedirectToAction("Index");
            else
                return View("Edit");
        }
        [HttpPost]
        public ActionResult EditEmployee1(EmployeeDetailsModel model)
        {
            int data = employeeRepository.Update1(model);
            if (data != 0)
                return RedirectToAction("Index");
            else
                return View("Edit1");
        }
        [HttpGet]
        public ActionResult Delete(Employee model)
        {
            Employee emp = employeeRepository.GetEmployee(model.EmpId);
            return View(emp);
        }
        [HttpPost]
        public ActionResult DeleteEmployee(Employee model)
        {
            int result = employeeRepository.DeleteEmployee(model.EmpId);
            if (result != 0)
                return RedirectToAction("Index");
            else  
                return View("Delete", result);
        }
    }
}