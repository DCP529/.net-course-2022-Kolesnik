using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Bogus.DataSets;

namespace Services
{
    public class TestDataGenerator
    {
        public List<Client> GenerateListClient()
        {
            List<Client> clients = new List<Client>();

            for (int i = 0; i <= 999; i++)
            {
                Faker<Client> generator = new Faker<Client>()
    .StrictMode(true)
    .RuleFor(x => x.FirstName, f => f.Name.FirstName())
    .RuleFor(x => x.LastName, f => f.Name.LastName())
    .RuleFor(x => x.Patronymic, f => "")
    .RuleFor(x => x.Passport, f => f.Random.Int(1, 500))
    .RuleFor(x => x.Phone, f => f.Random.Int(7700000, 7799999))
    .RuleFor(x => x.BirthDate, f => f.Date.Between(DateTime.Parse("01.01.1990"), DateTime.Now));

                clients.Add(generator.Generate());
            }

            return clients;
        }
        public List<Employee> GenerateListEmployee()
        {
            List<Employee> employees = new List<Employee>();

            for (int i = 0; i <= 999; i++)
            {
                Faker<Employee> generator = new Faker<Employee>()
    .StrictMode(true)
    .RuleFor(x => x.FirstName, f => f.Name.FirstName())
    .RuleFor(x => x.LastName, f => f.Name.LastName())
    .RuleFor(x => x.Patronymic, f => "")
    .RuleFor(x => x.Passport, f => f.Random.Int(1, 500))
    .RuleFor(x => x.Phone, f => f.Random.Int(7700000, 7799999))
    .RuleFor(x => x.Salary, f => f.Random.Decimal(1_000, 100_000))
    .RuleFor(x => x.Contract, f => "")
    .RuleFor(x => x.BirthDate, f => f.Date.Between(DateTime.Parse("01.01.1990"), DateTime.Now));

                employees.Add(generator.Generate());
            }

            return employees;
        }

        public Dictionary<int, Client> GenerateDictionaryClient()
        {
            Dictionary<int, Client> clients = new Dictionary<int, Client>();

            for (int i = 0; i <= 999; i++)
            {
                Faker<Client> generator = new Faker<Client>()
    .StrictMode(true)
    .RuleFor(x => x.FirstName, f => f.Name.FirstName())
    .RuleFor(x => x.LastName, f => f.Name.LastName())
    .RuleFor(x => x.Patronymic, f => "")
    .RuleFor(x => x.Passport, f => f.Random.Int(1, 1000))
    .RuleFor(x => x.Phone, f => i)
    .RuleFor(x => x.BirthDate, f => f.Date.Between(DateTime.Parse("01.01.1990"), DateTime.Now));

                var fakeClient = generator.Generate();
                clients.Add(fakeClient.Phone, fakeClient);
            }

            return clients;
        }
    }
}
