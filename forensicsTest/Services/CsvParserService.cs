using CsvHelper;
using forensicsTest.Mappers;
using forensicsTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;

namespace forensicsTest.Services
{
    public class CsvParserService : ICsvParserService
    {
        public object JsonConvert { get; private set; }

        public List<WebsiteModel> ReadCsvFileToWebsiteModel(string path)
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<WebsiteMap>();
                    
                    var records = csv.GetRecords<WebsiteModel>().ToList();
           
                    return records;
                }
            }
            catch (FieldValidationException e)
            {
                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}







