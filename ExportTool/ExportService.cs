using AutoMapper;
using CsvHelper;
using Models;
using Models.ModelsDb;
using Newtonsoft.Json;
using Services;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ExportTool
{
    public class ExportService
    {
        public async Task DataExportToFileAsync<T>(T person, string path) where T : Person
        {
            await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                        {
                            csv.WriteRecord(person);
                            csv.FlushAsync();
                        }
                    }
                }
            });
        }

        public async Task DataExportClientListAsync(List<Client> clients, string path)
        {
            await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                        {
                            csv.WriteRecordsAsync(clients);
                            csv.FlushAsync();
                        }
                    }
                }
            });
        }

        public async Task<List<Client>> DataExportToDatabaseAsync()
        {
            string path = Path.Combine("C:\\Users\\37377\\source\\repos\\.net-course-2022-Kolesnik\\ExportFiles", "ClientList");

            return await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        using (CsvReader reader = new CsvReader(sr, CultureInfo.CurrentCulture))
                        {
                            return Task.Run(() => (List<Client>)reader.GetRecordsAsync<List<Client>>());
                        }
                    }
                }
            });
        }

        public async Task SerializableExportToFileAsync<T>(T person, string path) where T : Person
        {
            await Task.Run(() =>
            {
                using (FileStream fs = new(path, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sr = new(fs))
                    {
                        sr.WriteAsync(JsonConvert.SerializeObject(person));
                    }
                }
            });
        }

        public async Task<T> DeserializableImportFromFile<T>(string path) where T : Person
        {
            return await Task.Run(() =>
            {
                using (FileStream fs = new(path, FileMode.Open))
                {
                    using (StreamReader sr = new(fs))
                    {
                        return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                    }
                }
            });
        }
    }
}