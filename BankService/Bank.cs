using Models;
using System;


namespace BankService
{
    public class Bank
    {
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
    }
}
