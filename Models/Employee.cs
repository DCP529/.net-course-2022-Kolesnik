using System;

namespace Models
{
    public class Employee : Person
    {
        public string Contract { get; set; }
        public decimal Salary { get; set; }
    }
}
