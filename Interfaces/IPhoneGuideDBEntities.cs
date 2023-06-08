using PhoneGuideApp.DB_Model.Tables;

namespace PhoneGuideApp.Interfaces
{
    public interface IPhoneGuideDBEntities
    {
        public List<Phone> GetPhones();
        public string RemovePhone(int id);
        bool IsEqualPhone(Phone phone);
        public string AddPhone(Phone phone);
        public string EditPhone(Phone editphone);
    }
}
