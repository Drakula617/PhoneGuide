using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PhoneGuideApp.DB_Model.Tables;
using PhoneGuideApp.Interfaces;

namespace PhoneGuideApp.DB_Model
{
    /// <summary>
    /// Класс для взаимодействия с базой данных phones.db
    /// </summary>
    public class PhoneGuideDBEntities: DbContext, IPhoneGuideDBEntities
    {
        public DbSet<Phone> Phones { get; set; }

        public PhoneGuideDBEntities(DbContextOptions<PhoneGuideDBEntities> options):base(options)
        {
            
        }
        //Переопределяем метод OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Используем "Ленивую подгрузку" данных
            optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=phones.db;");
            
        }

        /// <summary>
        /// Получение списка контактов
        /// </summary>
        /// <returns></returns>
        public List<Phone> GetPhones()
        {
            return Phones.ToList();
        }

        /// <summary>
        /// Удаление записи из списка контактов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RemovePhone(int id)
        {
            try
            {
                foreach (var item in Phones.ToDictionary(c => c.Id))
                {
                    if (item.Key == id)
                    {
                        Phones.Remove(item.Value);
                        SaveChanges();
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
        /// проверка контакта на уникальность</summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool IsEqualPhone(Phone phone)
        {
            if (Phones.ToList().Where(c => c.AddressStr == phone.AddressStr
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
        /// Добавление нового контакта
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public string AddPhone(Phone phone)
        {
            try
            {
                if (IsEqualPhone(phone) == false)
                {
                    Phones.Add(phone);
                    SaveChanges();
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
        /// Редактирование контактных данных
        /// </summary>
        /// <param name="editphone"></param>
        /// <returns></returns>
        public string EditPhone(Phone editphone)
        {
            try
            {
                foreach (var phone in Phones.ToDictionary(c => c.Id))
                {
                    if (editphone.Id == phone.Key)
                    {
                        Entry(phone.Value).CurrentValues.SetValues(editphone);
                        SaveChanges();
                        return "Изменения применены";
                    };
                }
                return "Объект не найден";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
