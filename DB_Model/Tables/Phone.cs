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
        
        public string HomeNumberPhone { get; set; }
        /// <summary>
        /// Номер мобильного телефона
        /// </summary>
        
        public string MobileNumberPhone { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Улица и номер дома
        /// </summary>
        public string StreetNumberHouse { get; set; }
        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string NumberApartment { get; set; }

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
                if (StreetNumberHouse != null)
                {
                    str += $", Ул.{StreetNumberHouse}";
                }
                if (NumberApartment != null)
                {
                    str += $", кв.{NumberApartment}";
                }
                
                return str.Trim();
            }

            
        }
    }
}
