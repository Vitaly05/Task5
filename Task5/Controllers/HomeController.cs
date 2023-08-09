using Microsoft.AspNetCore.Mvc;
using System.Text;
using Task5.Models;
using Task5.Utils;

namespace Task5.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("getUsersData")]
        public IActionResult GetUsersData(GeneratorConfigurationModel model)
        {
            FakeDataGenerator generator = new(model);
            var fakeData = generator.GenerateUsersData();
            return PartialView("_UsersData", fakeData);
        }

        [HttpPost("getCsvFile")]
        public IActionResult GetCsvFile(GeneratorConfigurationModel model)
        {
            var allPagesData = generateAllPages(model, model.Page);
            var csv = new CsvConverter<FakeUserDataModel>(allPagesData).GetCsvString();
            return File(Encoding.UTF8.GetBytes(csv.ToCharArray()), "text/csv", "FakeData.csv");
        }

        private List<FakeUserDataModel> generateAllPages(GeneratorConfigurationModel model, int pages)
        {
            var allPagesData = new List<FakeUserDataModel>();
            for (int i = 0; i <= pages; i++)
            {
                model.Page = i;
                FakeDataGenerator generator = new(model);
                allPagesData.AddRange(generator.GenerateUsersData());
            }
            return allPagesData;
        }
    }
}