using PhoneGuideApp.DB_Model.Tables;

namespace PhoneGuideApp.Interfaces
{
    public interface IUserContext
    {
        public byte[] ExportPhonesToCsv();
        public string ImportCSVPhones(List<Phone> phones);
    }
}
