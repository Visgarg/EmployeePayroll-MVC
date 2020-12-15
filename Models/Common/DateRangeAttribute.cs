using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeePayrollMVC.Models.Common
{
    public class DateRangeAttribute:RangeAttribute
    {
        public DateRangeAttribute(string minimum):base(typeof(DateTime),minimum,DateTime.Now.ToShortDateString())
        {

        }
    }
}