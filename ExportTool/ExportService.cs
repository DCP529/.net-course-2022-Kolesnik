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
        public void DataExportToFile<T>(T person, string path) where T : Person
        {           
            if (person is Client)
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
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
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
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

        public void DataExportClientList(List<Client> clients, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
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