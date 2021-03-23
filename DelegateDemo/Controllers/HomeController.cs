using DelegateDemo.Models;
using DelegateDemo.Utilities.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DelegateDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserService _UserService;
        public HomeController(ILogger<HomeController> logger, IUserService UserService)
        {
            _logger = logger;
            _UserService = UserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult RegisterUser(UserViewModel UserVM)
        {
            if (_UserService.AddUserUsingAdoNet(UserVM))
            //if (_UserService.AddUserUsingEF(UserVM))
            {
                return View("Index");
            }
            else
            {
                return View("Error",new ErrorViewModel());
            }
        }
    }
}
