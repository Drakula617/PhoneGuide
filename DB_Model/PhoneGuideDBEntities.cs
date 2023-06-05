using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PhoneGuideApp.DB_Model.Tables;

namespace PhoneGuideApp.DB_Model
{
    /// <summary>
    /// Класс для взаимодействия с базой данных PhoneGuideDB
    /// </summary>
    public class PhoneGuideDBEntities: DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public PhoneGuideDBEntities()
        {
            
        }
        //Переопределяем метод OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Используем "Ленивую подгрузку" данных
            optionsBuilder.UseLazyLoadingProxies().UseSqlite(new SqliteConnection()
            {
                ConnectionString = $"Data Source = {Directory.GetCurrentDirectory()}\\PhoneGuideDB.db"
            });
        }
    }
}
