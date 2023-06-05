using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneGuideApp.DB_Model.Tables
{

    [Table("Phone")]
    public class Phone
    {
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Fname { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Lname { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Mname { get; set; }
        /// <summary>
        /// Полное ФИО
        /// </summary>
        [Ignore]
        [NotMapped]
        public string FIO
        {
            get { return $"{Fname} {Lname} {Mname}".Trim(); }
        }
        /// <summary>
        /// Номер домашнего телефона
        /// </summary>
        [TypeConverter(typeof(StringConverter))]
        public string Home_number_phone { get; set; }
        /// <summary>
        /// Номер мобильного телефона
        /// </summary>
        [TypeConverter(typeof(StringConverter))]
        public string Mobile_number_phone { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Улица и номер дома
        /// </summary>
        public string Street_number_house { get; set; }
        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string Number_apartment { get; set; }

        /// <summary>
        /// Полный адрес
        /// </summary>
        [Ignore]
        [NotMapped]
        public string AddressStr
        {
            get
            { 
                string str = "";
                if (City != null)
                {
                    str +=$"г. {City}";
                }
                if (Street_number_house != null)
                {
                    str += $", Ул.{Street_number_house}";
                }
                if (Number_apartment != null)
                {
                    str += $", кв.{Number_apartment}";
                }
                
                return str.Trim();
            }

            
        }
    }
}
