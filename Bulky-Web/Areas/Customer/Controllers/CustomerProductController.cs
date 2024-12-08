using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky_Web.helpers;
using Microsoft.AspNetCore.Mvc;

namespace Bulky_Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class CustomerProductController : Controller
	{
		private readonly ArrangeQueryIncludeTypes _arrangeQueryIncludeTypes;
		private readonly IProductRepository _productRepository;
		private readonly string includeCategory = "Category";

		public CustomerProductController(ArrangeQueryIncludeTypes arrange, IProductRepository productRepository)
		{
			_arrangeQueryIncludeTypes = arrange;
			_productRepository = productRepository;
		}

		[HttpGet]
		public IActionResult Index()
		{
			ViewData["Title"] = "Product List";
			try
			{
				List<string> queryIncludeTypes = new List<string> { includeCategory };
				string arrangedIncludeTypes = _arrangeQueryIncludeTypes.ArrangeQueryInclude(queryIncludeTypes);
				List<Product> products = _productRepository.GetAll(arrangedIncludeTypes).ToList();
				ViewBag.Products = products;
				return View();
			}
			catch (Exception)
			{
				return RedirectToAction("Error", "Home");
			}
		}


        [HttpGet]
        public IActionResult ViewProduct(int? id)
        {
            ViewData["Title"] = "Product Overview";
            try
            {
				if(id == null)
				{
					return RedirectToAction("Index");
				}
                List<string> queryIncludeTypes = new List<string> { includeCategory };
                string arrangedIncludeTypes = _arrangeQueryIncludeTypes.ArrangeQueryInclude(queryIncludeTypes);
                Product? product = _productRepository.GetFirstOrDefault(p => p.Id == id,arrangedIncludeTypes);
				if(product == null)
				{
					TempData["Error"] = "Product not found";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
