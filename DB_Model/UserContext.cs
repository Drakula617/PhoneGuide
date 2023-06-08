using PhoneGuideApp.DB_Model.Tables;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using CsvHelper.Configuration;
using System;
using PhoneGuideApp.Interfaces;

namespace PhoneGuideApp.DB_Model
{
    /// <summary>
    /// Класс контекста данных
    /// </summary>
    public class UserContext: IUserContext
    {
        readonly IPhoneGuideDBEntities _phoneGuideDBEntities;
        public UserContext(IPhoneGuideDBEntities phoneGuideDBEntities)
        {
            _phoneGuideDBEntities = phoneGuideDBEntities;
        }

        /// <summary>
        /// Экспорт данных в CSV файл
        /// </summary>
        /// <returns></returns>
        public byte[] ExportPhonesToCsv()
        {
            var data = _phoneGuideDBEntities.GetPhones();
            var csvBytes = GenerateCsvBytes(data);
            return csvBytes;
        }

        /// <summary>
        /// Генерация массива байтов для экспорта
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] GenerateCsvBytes(List<Phone> data)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ";"
                };
                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
                    using (var csv = new CsvWriter(writer, config))
                    {
                        csv.Context.RegisterClassMap<PhoneExportMap>();
                        csv.WriteRecords(data);
                        writer.Flush();
                    }
                    return memoryStream.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Импорт данных из файла CSV
        /// </summary>
        /// <param name="phones"></param>
        /// <returns></returns>
        public string ImportCSVPhones(List<Phone> phones)
        {
            if (phones == null)
            {
                return "Данных нет";
            }
            else
            {
                try
                {
                    foreach (var item in phones)
                    {
                        item.Id = 0;
                        if (_phoneGuideDBEntities.IsEqualPhone(item) == false)
                        {
                            _phoneGuideDBEntities.AddPhone(item);
                        };
                    }
                    return "Данные импортированы";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
