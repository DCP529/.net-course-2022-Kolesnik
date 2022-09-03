using System;

namespace Models
{
    public class Employee : Person
    {
        public string Contract { get; set; }
        public decimal Salary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (!(obj is Client))
            {
                return false;
            }

            var result = (Employee)obj;

            return FirstName == result.FirstName
                && LastName == result.LastName
                && Patronymic == result.Patronymic
                && Passport == result.Passport
                && Phone == result.Phone
                && BirthDate == result.BirthDate
                && Contract == result.Contract
                && Salary == result.Salary;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode()
                + LastName.GetHashCode()
                + Passport.GetHashCode()
                + Patronymic.GetHashCode()
                + Phone.GetHashCode()
                + BirthDate.GetHashCode()
                + Contract.GetHashCode()
                + Salary.GetHashCode();
        }
    }
}
