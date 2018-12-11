﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmployeeWebserviceRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        EmployeeDataDataContext data = new EmployeeDataDataContext();
        public bool AddEmployee(Employee eml)
        {
            try
            {
                data.Employee.InsertOnSubmit(eml);
                data.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteEmployee(int idE)
        {
            try
            {
                Employee employeeToDelete = (from employee in data.Employee where employee.empId == idE select employee).Single();
                data.Employee.DeleteOnSubmit(employeeToDelete);
                data.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<Employee> GetProductList()
        {
            try
            {
                return (from employee in data.Employee select employee).ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateEmployee(Employee eml)
        {
            Employee employeeToModify = (from employee in data.Employee where employee.empId == eml.empId select employee).Single();
            employeeToModify.age = eml.age;
            employeeToModify.address = eml.address;
            employeeToModify.firstName = eml.firstName;
            employeeToModify.lastName = eml.lastName;
            data.SubmitChanges();
            return true;
        }
    }
}
