using AutoMapper;
using CsvHelper;
using Models;
using Models.ModelsDb;
using Services;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ExportTool
{
    public class ExportService
    {
        public void DataExportToFile(Person person)
        {
            DirectoryInfo directory = new DirectoryInfo("C:\\Users\\37377\\source\\repos\\.net-course-2022-Kolesnik\\ExportFiles");

            if (person is Client)
            {
                var client = (Client)person;

                string fullPath = Path.Combine(directory.FullName, "Client");

                using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                        {
                            csv.WriteField(nameof(client.FirstName));
                            csv.WriteField(nameof(client.LastName));
                            csv.WriteField(nameof(client.Patronymic));
                            csv.WriteField(nameof(client.BirthDate));
                            csv.WriteField(nameof(client.Passport));
                            csv.WriteField(nameof(client.Phone));
                            csv.WriteField(nameof(client.Bonus));
                            csv.WriteField(nameof(client.Id));

                            csv.NextRecord();

                            csv.WriteField(client.FirstName);
                            csv.WriteField(client.LastName);
                            csv.WriteField(client.Patronymic);
                            csv.WriteField(client.BirthDate);
                            csv.WriteField(client.Passport);
                            csv.WriteField(client.Phone);
                            csv.WriteField(client.Bonus);
                            csv.WriteField(client.Id);

                            csv.NextRecord();

                            csv.Flush();
                        }
                    }
                }
            }
            else if (person is Employee)
            {
                var employee = (Employee)person;

                string fullPath = Path.Combine(directory.FullName, "Employee");

                using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                        {
                            csv.WriteField(nameof(employee.FirstName));
                            csv.WriteField(nameof(employee.LastName));
                            csv.WriteField(nameof(employee.Patronymic));
                            csv.WriteField(nameof(employee.BirthDate));
                            csv.WriteField(nameof(employee.Passport));
                            csv.WriteField(nameof(employee.Phone));
                            csv.WriteField(nameof(employee.Bonus));
                            csv.WriteField(nameof(employee.Salary));
                            csv.WriteField(nameof(employee.Contract));
                            csv.WriteField(nameof(employee.Id));


                            csv.NextRecord();

                            csv.WriteField(employee.FirstName);
                            csv.WriteField(employee.LastName);
                            csv.WriteField(employee.Patronymic);
                            csv.WriteField(employee.BirthDate);
                            csv.WriteField(employee.Passport);
                            csv.WriteField(employee.Phone);
                            csv.WriteField(employee.Bonus);
                            csv.WriteField(employee.Contract);
                            csv.WriteField(employee.Salary);
                            csv.WriteField(employee.Id);

                            csv.NextRecord();

                            csv.Flush();
                        }
                    }
                }
            }
        }

        public void DataExportClientList(List<Client> clients)
        {
            DirectoryInfo directory = new DirectoryInfo("C:\\Users\\37377\\source\\repos\\.net-course-2022-Kolesnik\\ExportFiles");

            string fullPath = Path.Combine(directory.FullName, "ClientList");

            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                    {
                        var clientColumn = new Client();

                        csv.WriteField(nameof(clientColumn.FirstName));
                        csv.WriteField(nameof(clientColumn.LastName));
                        csv.WriteField(nameof(clientColumn.Patronymic));
                        csv.WriteField(nameof(clientColumn.BirthDate));
                        csv.WriteField(nameof(clientColumn.Passport));
                        csv.WriteField(nameof(clientColumn.Phone));
                        csv.WriteField(nameof(clientColumn.Bonus));

                        foreach (var client in clients)
                        {
                            csv.NextRecord();

                            csv.WriteField(client.FirstName);
                            csv.WriteField(client.LastName);
                            csv.WriteField(client.Patronymic);
                            csv.WriteField(client.BirthDate);
                            csv.WriteField(client.Passport);
                            csv.WriteField(client.Phone);
                            csv.WriteField(client.Bonus);
                        }

                        csv.NextRecord();

                        csv.Flush();
                    }
                }

            }
        }

        public void DataExportToDatabase()
        {
            string path = Path.Combine("C:\\Users\\37377\\source\\repos\\.net-course-2022-Kolesnik\\ExportFiles", "Client"); 

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    using (var reader = new CsvReader(sr, CultureInfo.CurrentCulture))
                    {
                        reader.Read();

                        Client readClient = reader.GetRecord<Client>();

                        //var clientConfig = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDb>());

                        //var clientMapper = new Mapper(clientConfig);

                        //var clientDb = clientMapper.Map<ClientDb>(client);
                        //clientDb.Id = Guid.NewGuid();

                        var clientService = new ClientService(new BankDbContext());

                        clientService.AddClient(readClient);
                    }
                }
            }

        }
    }
}