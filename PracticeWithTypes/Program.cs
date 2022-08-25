using Models;
using System;
using BankService;

namespace PracticeWithTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            Client client = new Client() { FirstName = "Владислав", LastName="Богорош", Patronymic = "Владимирович", BirthDate = DateTime.Parse("11.06.22"), Passport = 1 };
            var employee = bank.ConvertClientToEmployee(client);
            Console.WriteLine(employee.Passport);

            UpdateContactFromEmployee(employee);
            Console.WriteLine(employee.Contract);

            employee.Contract = UpdateContactFromString("Олег", "Диордиев", "Михайлович", DateTime.Parse("01.05.22"), 12);
            Console.WriteLine(employee.Contract);

            var rub = UpdateCurrency("Рубль", 16.35M, 0809);

            Console.WriteLine(rub.Name);
            Console.ReadLine();
        }

        public static void UpdateContactFromEmployee(Employee employee) // неправильный метод
        {
            employee.Contract = $"{employee.FirstName} {employee.LastName} {employee.Patronymic}," +
                $" родившийся {employee.BirthDate}, с пасспартом {employee.Passport} принят на работу в Dex!";
        }

        public static string UpdateContactFromString(string firstName, string lastName, string patronymic, DateTime dataTime, int passport) // правильный метод
        {
            return $"{firstName} {lastName} {patronymic}, родившийся {dataTime}, с пасспартом {passport} принят на работу в Dex!";
        }

        public static Currency UpdateCurrency(string name, decimal cost, int code)
        {
            return new Currency() { Name = name, Code = code, Cost = cost};
        }
    }
}
