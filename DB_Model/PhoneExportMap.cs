using CsvHelper.Configuration;
using PhoneGuideApp.DB_Model.Tables;
using System;

namespace PhoneGuideApp.DB_Model
{
    public class PhoneExportMap: ClassMap<Phone>
    {
        public PhoneExportMap()
        {
            Map(m => m.Id);
            Map(m => m.Fname);
            Map(m => m.Lname);
            Map(m => m.Mname);
            Map(m => m.HomeNumberPhone);
            Map(m => m.MobileNumberPhone);
            Map(m => m.City);
            Map(m => m.StreetNumberHouse);
            Map(m => m.NumberApartment);
            Map(m => m.FIO).Ignore();
            Map(m => m.AddressStr).Ignore();
        }
    }
}
