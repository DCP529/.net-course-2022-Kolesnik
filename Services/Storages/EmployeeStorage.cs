using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.ModelsDb;
using Services.Storage;
using System.Net;

namespace Services
{
    public class EmployeeStorage : IEmployeeStorage
    {
        public BankDbContext Data { get; }

        public EmployeeStorage()
        {
            Data = new BankDbContext();
        }

        public void Add(EmployeeDb employee)
        {
            Data.Employees.Add(employee);
        }

        public void Update(Guid id, EmployeeDb employee)
        {
            var getEmployee = Data.Employees.FirstOrDefault(x => x.Id == employee.Id);

            Data.Entry(getEmployee).CurrentValues.SetValues(employee);

            Data.Add(employee);
        }

        public void Delete(Guid id)
        {
            Data.Employees.Remove(Data.Employees.FirstOrDefault(x => x.Id == id));
        }
    }
}
