using PhoneGuideApp.DB_Model.Tables;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using CsvHelper.Configuration;
using System;

namespace PhoneGuideApp.DB_Model
{
    /// <summary>
    /// Класс контекста данных
    /// </summary>
    public class UserContext
    {
        public UserContext()
        {
            
        }
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <returns></returns>
        public List<Phone> GetData()
        {
            return App.db.Phones.ToList();
        }

        /// <summary>
        /// Удаление строки в таблице Phones
        /// </summary>
        /// <param name="id"></param>
        public string RemovePhone(int id)
        {
            try
            {
                foreach (var item in App.db.Phones.ToDictionary(c=> c.Id))
                {
                    if(item.Key == id)
                    {
                        App.db.Phones.Remove(item.Value);
                        App.db.SaveChanges();
                        return "Объект удален";
                    }
                }
                return "Объект не найден";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        /// <summary>
        /// Проверка на уникальность ФИО и адреса
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        bool IsEqualPhone(Phone phone)
        {
            if (App.db.Phones.ToList().Where(c => c.AddressStr == phone.AddressStr
            && c.FIO == phone.FIO).Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Добавление новой записи в таблицу Phones
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public string AddPhone(Phone phone)
        {
            try
            {
                if (IsEqualPhone(phone) == false)
                {
                    App.db.Phones.Add(phone);
                    App.db.SaveChanges();
                    return "Запись добавлена";
                }
                else
                {
                    return "Запись с таким ФИО и адресом уже существует";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="editphone"></param>
        public string EditPhone(Phone editphone)
        {
            try
            {
                foreach (var phone in App.db.Phones.ToDictionary(c => c.Id))
                { 
                    if (editphone.Id == phone.Key)
                    {
                        App.db.Entry(phone.Value).CurrentValues.SetValues(editphone);
                        App.db.SaveChanges();
                        return "Изменения применены";
                    };
                }
                return "Объект не найден";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }

        /// <summary>
        /// Преобразование List<Phone> в байты
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] GenerateCsvBytes(List<Phone> data)
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
                    using (var writer = new StreamWriter(memoryStream,Encoding.UTF8))
                    using (var csv = new CsvWriter(writer, config))
                    {
                        csv.Context.RegisterClassMap<PhoneExportMap>();
                        csv.WriteRecords(data);
                        writer.Flush();
                    }
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                return null;
                    
            }

        }
        /// <summary>
        /// Экспорт List<Phone> в CSV формат
        /// </summary>
        /// <returns></returns>
        public byte[] ExportPhonesToCsv()
        {
            var data = GetData();
            var csvBytes = GenerateCsvBytes(data);
            return csvBytes;
        }

        /// <summary>
        /// Импорт CSV в List<Phone>
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
                        if (IsEqualPhone(item) == false)
                        {
                            App.db.Phones.Add(item);
                            App.db.SaveChanges();
                            
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
