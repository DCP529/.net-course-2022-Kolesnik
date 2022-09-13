using Models;
using System;
using System.Collections.Generic;


namespace Services
{
    public class BankService
    {
        private List<Person> BlackList { get; set; }

        public decimal Payroll(decimal bankProfit, decimal expenses, params Employee[] owners)
        {
            var result = bankProfit - expenses / owners.Length;
            return result;
        }

        public Employee ConvertClientToEmployee(Client client)
        {
            Employee employee = new Employee() 
            { 
                FirstName = client.FirstName,
                LastName = client.LastName,
                Passport = client.Passport,
                BirthDate = client.BirthDate,
                Patronymic = client.Patronymic
            };

            return employee;
        }

        public void AddBonus(Person person)
        {
            person.Bonus += 1;
        }

        public void AddToBlackList<T>(T person) where T : Person
        {
            BlackList.Add(person);
        }

        public bool IsPersonInBlackList<T>(T person) where T : Person
        {
            return BlackList.Contains(person);
        }
    }
}
