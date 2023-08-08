using Microsoft.AspNetCore.Mvc;
using Task5.Models;
using Task5.Utils;

namespace Task5.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var fakeData = new FakeDataGenerator(new GeneratorConfigurationModel()).GenerateUsersData();
            return View(fakeData);
        }

        [HttpPost("getUsersData")]
        public IActionResult GetUsersData(GeneratorConfigurationModel model)
        {
            FakeDataGenerator generator = new(model);
            var fakeData = generator.GenerateUsersData();
            return PartialView("_UsersData", fakeData);
        }
    }
}