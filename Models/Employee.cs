using System;

namespace Models
{
    public class Employee : Person
    {
        public int Passport { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Contract { get; set; }
    }
}
