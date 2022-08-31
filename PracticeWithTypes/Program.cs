using Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDataGenerator dataGenerator = new TestDataGenerator();

            #region ListSearch
            List<Client> clientsList = new List<Client>();
            clientsList = dataGenerator.GenerateListClient();

            Client testClient = clientsList[50];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var resultClientList = clientsList.FirstOrDefault(c => c.Phone == testClient.Phone);
            stopwatch.Stop();

            Console.WriteLine(resultClientList.FirstName);
            Console.WriteLine($"Время затраченнное на поиск по списку = {stopwatch.ElapsedTicks}\n");
            #endregion

            #region DictionarySearch
            var clientsDictionary = new Dictionary<int, Client>();
            clientsDictionary = dataGenerator.GenerateDictionaryClient();

            testClient = clientsDictionary[50];


            stopwatch.Restart();

            var resultClientDictionary = clientsDictionary.FirstOrDefault(c => c.Key == testClient.Phone);

            stopwatch.Stop();


            Console.WriteLine(resultClientDictionary.Value.FirstName);

            Console.WriteLine($"Время затраченнное на поиск по словарю = {stopwatch.ElapsedTicks}\n");
            #endregion

            #region BabyClient
            Console.WriteLine("Поиск клиентов возраст которых меньше 10 лет");

            var baby = clientsList.Where(b => b.BirthDate >= DateTime.Parse("01.01.2012")).ToList();

            foreach (var item in baby)
            {
                Console.WriteLine(item.FirstName);
            }
            #endregion

            #region minSalary
            List<Employee> employees = new List<Employee>();
            employees = dataGenerator.GenerateListEmployee();

            var minEmployeeSalary = employees.Min(e => e.Salary);

            var resultEmployeeList = employees.Where(e => e.Salary == minEmployeeSalary).ToList();

            Console.WriteLine("\nСотрудники с минимальной зп:");
            foreach (var item in resultEmployeeList)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName);
            }

            Console.WriteLine($"Минимальная зп составляет: {minEmployeeSalary}\n");
            #endregion

            #region searchFirstOrDefault
            stopwatch.Reset();
            stopwatch.Start();

            var firstOrDefount = clientsDictionary.FirstOrDefault(c => c.Key == clientsDictionary.Count - 1);
            
            stopwatch.Stop();

            Console.WriteLine("Имя последнего клиента коллекции = " + firstOrDefount.Value.FirstName
                + "а время его поиска заняло " + stopwatch.ElapsedTicks);
            #endregion

            stopwatch.Reset();
            stopwatch.Start();

            var lastClient = clientsDictionary[clientsDictionary.Count - 1];

            stopwatch.Stop();

            Console.WriteLine($"Поиск последнего элемента коллекции по ключу занял: {stopwatch.ElapsedTicks}");
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

        public static Currency UpdateCurrency(string name, int code)
        {
            return new Currency() { Name = name, Code = code };
        }
    }
}
