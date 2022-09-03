using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Bogus.DataSets;
using Currency = Models.Currency;

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
    .RuleFor(x => x.Phone, f => 77500000 + i)
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
    .RuleFor(x => x.Phone, f => 77500000 + i)
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
    .RuleFor(x => x.Phone, f => 77500000 + i)
    .RuleFor(x => x.BirthDate, f => f.Date.Between(DateTime.Parse("01.01.1990"), DateTime.Now));

                var fakeClient = generator.Generate();
                clients.Add(fakeClient.Phone, fakeClient);
            }

            return clients;
        }

        public Dictionary<Client, List<Account>> GenerateDictionaryClientAccount()
        {
            Dictionary<Client, List<Account>> clients = new Dictionary<Client, List<Account>>();

            List<string> fakeCurrencyName = new List<string>()
                {
                    "Rub",
                    "Lei",
                    "Eu",
                    "Ua"
                };


            for (int i = 0; i <= 999; i++)
            {
                List<Account> fakeAccountList = new List<Account>();

                for (int j = 0; j < 2; j++)
                {
                    var currency = new Currency()
                    {
                        Name = fakeCurrencyName[new Random().Next(fakeCurrencyName.Count)],
                        Code = new Random().Next(1000)
                    };

                    Faker<Account> generatorAccount = new Faker<Account>()
                    .StrictMode(true)
                    .RuleFor(x => x.Amount, c => c.Random.Int(1, 1000))
                    .RuleFor(x => x.Currency, c => currency);

                    fakeAccountList.Add(generatorAccount.Generate());
                }
                Faker<Client> generatorClient = new Faker<Client>()
    .StrictMode(true)
    .RuleFor(x => x.FirstName, f => f.Name.FirstName())
    .RuleFor(x => x.LastName, f => f.Name.LastName())
    .RuleFor(x => x.Patronymic, f => "")
    .RuleFor(x => x.Passport, f => f.Random.Int(1, 1000))
    .RuleFor(x => x.Phone, f => 77500000 + i)
    .RuleFor(x => x.BirthDate, f => f.Date.Between(DateTime.Parse("01.01.1990"), DateTime.Now));

                var fakeClient = generatorClient.Generate();

                clients.Add(fakeClient, fakeAccountList);
            }

            return clients;
        }
    }
}
