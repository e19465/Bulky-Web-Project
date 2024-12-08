using System.Diagnostics;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky_Web.helpers;
using Microsoft.AspNetCore.Mvc;

namespace Bulky_Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
		}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [Route("/Home/Error/")]
        public IActionResult Error()
        {
            return View();
        }

        [Route("/StatusCodeError/{statusCode}")]
        public IActionResult StatusCodeError(int statusCode)
        {
            ViewBag.Status = statusCode;
            if (statusCode == 404)
            {
                ViewBag.ErrorMessage = "The page you are looking for does not exist.";
            }
            else
            {
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
            }
            return View();
        }
    }
}
