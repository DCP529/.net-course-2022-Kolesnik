﻿using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Services.Storage;

namespace Services
{
    public class EmployeeStorage : IEmployeeStorage
    {
        public List<Employee> Data { get; }

        public EmployeeStorage()
        {
            Data = new List<Employee>();
        }

        public void Add(Employee employee)
        {
            Data.Add(employee);
        }

        public void Update(Employee employee)
        {
            var result = Data.Find(x => x.Passport == employee.Passport);

            Data.Remove(result);
            Data.Add(employee);
        }

        public void Delete(Employee employee)
        {
            Data.Remove(employee);
        }
    }
}
