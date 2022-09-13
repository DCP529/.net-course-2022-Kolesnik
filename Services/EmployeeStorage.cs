using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class EmployeeStorage
    {
        public readonly List<Employee> employees = new List<Employee>();

        public void Add(Employee employee)
        {
            employees.Add(employee);
        }
    }
}
