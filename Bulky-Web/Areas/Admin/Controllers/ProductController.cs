using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky_Web.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly FileUploadService _fileUploadService;
        private readonly ArrangeQueryIncludeTypes _arrangeQueryIncludeTypes;
        private readonly string includeCategory = "Category";

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, FileUploadService fileUploadService, ArrangeQueryIncludeTypes arrange)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
			_fileUploadService = fileUploadService;
            _arrangeQueryIncludeTypes = arrange;
		}

        private IEnumerable<SelectListItem> GetSelectListCategories()
		{
			IEnumerable<SelectListItem> categories = _categoryRepository.GetAll().Select(cat => new SelectListItem
			{
				Text = cat.Name,
				Value = cat.Id.ToString()
			});
			return categories;
		}

		/// <summary>
		/// This action is responsible for fetching all products from the database and display them in the view
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Product List";
            try
            {
                List<String> queryIncludeTypes = new List<string> { includeCategory };
                string includeProperties = _arrangeQueryIncludeTypes.ArrangeQueryInclude(queryIncludeTypes);
                List<Product> products = _productRepository.GetAll(includeProperties).ToList();
				ViewBag.Products = products;
                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error occur while fetching products";
                Console.WriteLine($"Error occurred while fetching products: {ex.Message}");
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// This action responsible for showing the form to create a new product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create Product";
            try
            {
                IEnumerable<SelectListItem> categories = GetSelectListCategories();
                ViewBag.Categories = categories;
                return View();
            }
            catch (Exception e)
            {
                TempData["error"] = "Error occur while fetching categories";
                Console.WriteLine($"Error occurred while fetching categories: {e.Message}");
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// This is a post action to create a new product
        /// </summary>
        /// <param name="product">The details of the product to create</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Product product, IFormFile? file)
        {
            try
            {
                ViewData["Title"] = "Create Product";
                if (ModelState.IsValid)
                {
					if (file != null)
					{
						product.ImageUrl = _fileUploadService.UploadImage(file, "product");
					}
					_productRepository.Add(product);
                    _productRepository.Save();
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    IEnumerable<SelectListItem> categories = GetSelectListCategories();
                    ViewBag.Categories = categories;
                    TempData["error"] = "Invalid data for create product";
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Error occur while creating product";
                Console.WriteLine($"Error occurred while creating product: {e.Message}");
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// This action responsible for showing the form to update a product
        /// </summary>
        /// <param name="id">Integer type ID of the product</param>
        /// <returns>If product exists, return it to View</returns>
        [HttpGet]
        public IActionResult Update(int? id)
        {
            ViewData["Title"] = "Update Product";
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Product? foundProduct = _productRepository.GetFirstOrDefault(x => x.Id == id);
                if (foundProduct == null)
                {
                    TempData["error"] = "The product you are trying to update does not exist";
                    return RedirectToAction("Index");
                }
				IEnumerable<SelectListItem> categories = GetSelectListCategories();
				ViewBag.Categories = categories;
				return View(foundProduct);
            }
            catch (Exception e)
            {
                TempData["error"] = "Error occur while fetching product to update";
                Console.WriteLine($"Error occurred while updating product: {e.Message}");
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// This action is responsible for updating the product in the database
        /// </summary>
        /// <param name="product">Updated product to save in the DB</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(Product product, IFormFile? file)
        {
            try
            {
                if (file != null)
                {
                    string? existingImage = product.ImageUrl;
                    if(existingImage != null)
                    {
                        _fileUploadService.RemoveImage(existingImage);
                    }
                    product.ImageUrl = _fileUploadService.UploadImage(file, "product");
                }
                _productRepository.Update(product);
                _productRepository.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
				IEnumerable<SelectListItem> categories = GetSelectListCategories();
				ViewBag.Categories = categories;
				TempData["error"] = "Error occur while updating product, Please try again later";
                Console.WriteLine($"Error occurred while updating product: {e.Message}");
                return View();
            }
        }



        /// <summary>
        /// This action responsible for deleting the product from the database
        /// </summary>
        /// <param name="id">The ID of the product to be deleted</param>
        /// <returns>If produc exists, returns it to View</returns>
        public IActionResult Delete(int? id)
        {
            ViewData["Title"] = "Delete Product";
            try
            {
                if(id == null)
                {
                    return RedirectToAction("Index");
                }
                Product? product = _productRepository.GetFirstOrDefault(x => x.Id == id);
                if(product  == null)
                {
                    TempData["error"] = "The product you are trying to delete does not exist";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch(Exception e)
            {
                TempData["error"] = "Error occur while fetching the product to be deleted";
                Console.WriteLine($"Error occurred while fetching product to delete: {e.Message}");
                return View();
            }
        }


        /// <summary>
        /// This action responsible for deleting the product from the database
        /// </summary>
        /// <param name="product">The product to be deleted</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            try
            {
                _productRepository.Remove(product);
                _productRepository.Save();
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error occur while deleting product";
                Console.WriteLine($"Error occurred while deleting product: {e.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
