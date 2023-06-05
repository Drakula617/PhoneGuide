using CsvHelper.Configuration;
using PhoneGuideApp.DB_Model.Tables;
using System;

namespace PhoneGuideApp.DB_Model
{
    public class PhoneExportMap: ClassMap<Phone>
    {
        public PhoneExportMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Fname).Name("Fname");
            Map(m => m.Lname).Name("Lname");
            Map(m => m.Mname).Name("Mname");
            Map(m => m.Home_number_phone).Name("Home_number_phone");
            Map(m => m.Mobile_number_phone).Name("Mobile_number_phone");
            Map(m => m.City).Name("City");
            Map(m => m.Street_number_house).Name("Street_number_house");
            Map(m => m.Number_apartment).Name("Number_apartment");
            Map(m => m.FIO).Ignore();
            Map(m => m.AddressStr).Ignore();
        }
    }
}
