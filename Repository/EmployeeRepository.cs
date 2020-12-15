using EmployeePayrollMVC.Models;
using EmployeePayrollMVC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeePayrollMVC.Repository
{
    public class EmployeeRepository
    {
        EmployeeContext dbContext = new EmployeeContext();
        public bool RegisterEmployee(RegisterRequestModel employee)
        {

            try
            {
                Employee validEmployee = dbContext.Employees.Where(x => x.Name == employee.Name && x.Gender == employee.Gender).FirstOrDefault();
                if (validEmployee == null)
                {
                    int departmentId = dbContext.Departments.Where(x => x.DeptName == employee.Department).Select(x => x.DeptId).FirstOrDefault();
                    Employee newEmployee = new Employee()
                    {
                        Name = employee.Name,
                        Gender = employee.Gender,
                        DepartmentId = departmentId,
                        SalaryId = Convert.ToInt32(employee.SalaryId),
                        StartDate = employee.StartDate,
                        Description = employee.Description
                    };
                    Employee returnData = dbContext.Employees.Add(newEmployee);
                }
                int result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<EmployeeDetailsModel> GetEmployees()
        {
            try
            {
                List<EmployeeDetailsModel> list = (from e in dbContext.Employees
                                                   join d in dbContext.Departments on e.DepartmentId equals d.DeptId
                                                   join s in dbContext.Salaries on e.SalaryId equals s.SalaryId
                                                   select new EmployeeDetailsModel
                                                   {
                                                       EmpId = e.EmpId,
                                                       Name = e.Name,
                                                       Gender = e.Gender,
                                                       DepartmentId = d.DeptId,
                                                       Department = d.DeptName,
                                                       SalaryId = s.SalaryId,
                                                       Amount = s.Amount,
                                                       Description = e.Description,
                                                       StartDate = e.StartDate
                                                   }).ToList<EmployeeDetailsModel>();
                return list;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public Employee GetEmployee(int id)
        {
            try
            {
                Employee list = dbContext.Employees.Where(x => x.EmpId == id).SingleOrDefault();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public EmployeeDetailsModel GetEmployee1(int id)
        {
            try
            {
                //Employee list = dbContext.Employees.Where(x => x.EmpId == id).SingleOrDefault();
                EmployeeDetailsModel list = (from e in dbContext.Employees
                                             join d in dbContext.Departments on e.DepartmentId equals d.DeptId
                                             join s in dbContext.Salaries on e.SalaryId equals s.SalaryId
                                             where e.EmpId == id
                                             select new EmployeeDetailsModel
                                             {
                                                 EmpId = e.EmpId,
                                                 Name = e.Name,
                                                 Gender = e.Gender,
                                                 DepartmentId = d.DeptId,
                                                 Department = d.DeptName,
                                                 SalaryId = s.SalaryId,
                                                 Amount = s.Amount,
                                                 Description = e.Description,
                                                 StartDate = e.StartDate
                                             }).SingleOrDefault();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public int Update(Employee model)
        {
            var data = dbContext.Employees.FirstOrDefault(x => x.EmpId == model.EmpId);
            //checking if any such record exists in database
            if (data != null)
            {
                data.EmpId = model.EmpId;
                data.Name = model.Name;
                data.Gender = model.Gender;
                data.DepartmentId = model.DepartmentId;
                data.Department = model.Department;
                data.SalaryId = model.SalaryId;
                data.Salary = model.Salary;
                data.StartDate = model.StartDate;
                data.Description = model.Description;
                return dbContext.SaveChanges();
            }
            else
                return 0;
        }
        public int Update1(EmployeeDetailsModel model)
        {
            var data = dbContext.Employees.FirstOrDefault(x => x.EmpId == model.EmpId);
            //checking if any such record exists in database
            if (data != null)
            {
                int departmentId = dbContext.Departments.Where(x => x.DeptName == model.Department).Select(x => x.DeptId).FirstOrDefault();
                data.EmpId = model.EmpId;
                data.Name = model.Name;
                data.Gender = model.Gender;
                data.DepartmentId = departmentId;
                data.SalaryId = model.SalaryId;
                data.StartDate = model.StartDate;
                data.Description = model.Description;
                return dbContext.SaveChanges();
            }
            else
                return 0;
        }
        public int DeleteEmployee(int id)
        {
            try
            {
                Employee data = dbContext.Employees.Where(x => x.EmpId == id).SingleOrDefault();
                if (data != null)
                {
                    dbContext.Employees.Remove(data);
                    return dbContext.SaveChanges();
                }
                else
                    return 0;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}