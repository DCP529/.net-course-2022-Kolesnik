using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class BankService
    {
        private List<Person> BlackList = new List<Person>();

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

        public async Task AddToBlackListAsync<T>(T person) where T : Person
        {
            await Task.Run(() =>BlackList.Add(person));
        }

        public async Task<bool> IsPersonInBlackListAsync<T>(T person) where T : Person
        {
            return await Task.Run(() =>BlackList.Contains(person));
        }
    }
}
