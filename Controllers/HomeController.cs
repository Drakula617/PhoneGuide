using Microsoft.AspNetCore.Mvc;
using PhoneGuideApp.DB_Model;
using PhoneGuideApp.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using PhoneGuideApp.DB_Model.Tables;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Text;
using CsvHelper;
using PhoneGuideApp.Interfaces;

namespace PhoneGuideApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        readonly IUserContext _userContext;
        readonly IPhoneGuideDBEntities _phoneGuideDBEntities;
        public HomeController(ILogger<HomeController> logger, IUserContext userContext, IPhoneGuideDBEntities phoneGuideDBEntities)
        {
            _logger = logger;
            _userContext = userContext;
            _phoneGuideDBEntities = phoneGuideDBEntities;
        }

        /// <summary>
        /// Загрузка страницы
        /// </summary>
        /// <returns></returns>
        public IActionResult PhonesPage()
        {
            return View();
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPhones()
        {
            return Json(_phoneGuideDBEntities.GetPhones());
        }

        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="newphone"></param>
        /// <returns></returns>
        public IActionResult AddPhone([FromBody] Phone newphone) 
        {
            return Content(_phoneGuideDBEntities.AddPhone(newphone));
        }

        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="editphone"></param>
        /// <returns></returns>
        public IActionResult EditPhone([FromBody] Phone editphone)
        {
            _phoneGuideDBEntities.EditPhone(editphone);
             return Content("Изменения сохранены");
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult RemovePhone(int id) 
        {
            return Content(_phoneGuideDBEntities.RemovePhone(id));
        }

        /// <summary>
        /// Экспорт данных
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportPhonesToCSV()
        {
            return File(_userContext.ExportPhonesToCsv(), "text/csv",fileDownloadName:$"Phones-{DateTime.Now.ToString("g")}.csv");
        }

        /// <summary>
        /// Импорт данных
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult ImportPhonesFromCSV([FromForm]IFormFile file)
        {
            var list = App.ConvertCsvToObjects<Phone>(file);
            return Content(_userContext.ImportCSVPhones(list));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}