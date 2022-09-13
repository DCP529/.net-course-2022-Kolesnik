using System;

namespace Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Passport { get; set; }
        public int Phone { get; set; }
        public DateTime BirthDate { get; set; }

        public int Bonus { get; set; }
    }
}
