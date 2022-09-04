﻿using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class EmployeeService
    {
        private List<Employee> _employees;

        public void AddEmployee(Employee employee)
        {
            try
            {
                if (employee.BirthDate > DateTime.Parse("31.12.2004"))
                {
                    throw new AgeLimitException("Возраст клиента должен быть больше 18!");
                }

                if (employee.Passport == 0)
                {
                    throw new PassportNullException("Нельзя добавить клиента без паспортных данных!");
                }

                _employees.Add(employee);

            }
            catch (AgeLimitException ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
            catch (PassportNullException passportEx)
            {
                Console.WriteLine(passportEx.Message);

                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw;
            }

        }
    }
}
