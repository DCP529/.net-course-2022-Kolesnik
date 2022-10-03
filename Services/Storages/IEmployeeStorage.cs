using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Models.ModelsDb;

namespace Services.Storage
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
        public List<Employee> Data { get; }
    }
}
