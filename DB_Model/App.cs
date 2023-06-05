using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace PhoneGuideApp.DB_Model
{
    public static class App
    {
        public static PhoneGuideDBEntities db = new PhoneGuideDBEntities();
        public static UserContext context = new UserContext();
        public static List<T> ConvertCsvToObjects<T>(IFormFile file) where T : class
        {
            if (file == null)
            {
                return null;
            }
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, 
                    Delimiter = ";",
                    MissingFieldFound = null
                };
                using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();
                    var records = csv.GetRecords<T>().ToList();
                    return records;
                }
            }
            catch
            {
                return null;
            }

        }
    }
}
