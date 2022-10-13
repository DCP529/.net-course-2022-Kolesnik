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
        public async Task DataExportToFileAsync<T>(T person, string path) where T : Person
        {
            if (person is Client)
            {
                await using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await using (StreamWriter sw = new StreamWriter(fs))
                    {
                        await using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                        {
                            await Task.Run(() => csv.WriteRecord(person));
                            await csv.FlushAsync();
                        }
                    }
                }
            }
            else
            {
                await using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await using (StreamWriter sw = new StreamWriter(fs))
                    {
                        await using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                        {
                            await Task.Run(() => csv.WriteRecord(person));
                            await csv.FlushAsync();
                        }
                    }
                }
            }
        }

        public async Task DataExportClientListAsync(List<Client> clients, string path)
        {
            await using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                await using (StreamWriter sw = new StreamWriter(fs))
                {
                    await using (CsvWriter csv = new CsvWriter(sw, CultureInfo.CurrentCulture))
                    {
                        await csv.WriteRecordsAsync(clients);
                        await csv.FlushAsync();
                    }
                }

            }
        }

        public async Task<List<Client>> DataExportToDatabaseAsync()
        {
            string path = Path.Combine("C:\\Users\\37377\\source\\repos\\.net-course-2022-Kolesnik\\ExportFiles", "ClientList");

            await using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    using (CsvReader reader = new CsvReader(sr, CultureInfo.CurrentCulture))
                    {
                        return await Task.Run(() => (List<Client>)reader.GetRecordsAsync<List<Client>>());
                    }
                }
            }

        }
    }
}