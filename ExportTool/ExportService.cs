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
        public void DataExportToFile<T>(T person) where T : Person
        {
            DirectoryInfo directory = new DirectoryInfo("C:\\Users\\37377\\source\\repos\\.net-course-2022-Kolesnik\\ExportFiles");

            if (person is Client)
            {
                string fullPath = Path.Combine(directory.FullName, "Client");

                using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                        {
                            csv.WriteRecord(person);
                            csv.Flush();
                        }
                    }
                }
            }
            else
            {
                string fullPath = Path.Combine(directory.FullName, "Employee");

                using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                        {
                            csv.WriteRecord(person);
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
                        csv.WriteRecords(clients);
                        csv.Flush();
                    }
                }

            }
        }

        public List<Client> DataExportToDatabase()
        {
            string path = Path.Combine("C:\\Users\\37377\\source\\repos\\.net-course-2022-Kolesnik\\ExportFiles", "ClientList"); 

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    using (var reader = new CsvReader(sr, CultureInfo.CurrentCulture))
                    {
                        return reader.GetRecords<Client>().ToList();                        
                    }
                }
            }

        }
    }
}