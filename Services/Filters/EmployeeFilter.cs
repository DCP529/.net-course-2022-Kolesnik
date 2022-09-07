using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Filters
{
    public class EmployeeFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Phone { get; set; }
        public int Passport { get; set; }
        public string Contract { get; set; }
        public decimal Salary { get; set; }

        public DateTime[] BirthDayRange { get; set; } = new DateTime[2];
    }
}
