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
        private readonly IProductRepository _productRepository;
        private readonly ArrangeQueryIncludeTypes _arrangeQueryIncludeTypes;
        private readonly string includeCategory = "Category";

		public HomeController(ILogger<HomeController> logger, IProductRepository product, ArrangeQueryIncludeTypes arrange)
        {
            _logger = logger;
            _productRepository = product;
			_arrangeQueryIncludeTypes = arrange;
		}

        public IActionResult Index()
        {
            try
            {
                List<string> includeTypes = new List<string> { includeCategory };
				string includeString = _arrangeQueryIncludeTypes.ArrangeQueryInclude(includeTypes);
				List<Product> products = _productRepository.GetAll(includeString).ToList();
                ViewBag.Products = products;    
				return View();
			}
            catch (Exception)
            {
                return RedirectToAction("Home", "Error");

			}
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
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
