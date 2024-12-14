using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky_Web.Config;
using Bulky_Web.helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Role_Admin)]
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
            catch (Exception)
            {
                TempData["error"] = "Error occur while fetching categories";
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
            catch (Exception)
            {
                TempData["error"] = "Error occur while creating product";
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
            catch (Exception)
            {
                TempData["error"] = "Error occur while fetching product to update";
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
                    if (existingImage != null)
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
            catch (Exception)
            {
                IEnumerable<SelectListItem> categories = GetSelectListCategories();
                ViewBag.Categories = categories;
                TempData["error"] = "Error occur while updating product, Please try again later";
                return View();
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<string> queryIncludeTypes = new List<string> { includeCategory };
                string includeProperties = _arrangeQueryIncludeTypes.ArrangeQueryInclude(queryIncludeTypes);
                List<Product> products = _productRepository.GetAll(includeProperties).ToList();
                return StatusCode(200, new { data = products });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Error while retrieving products" });
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return StatusCode(400, new { success = false, error = "Invalid product ID" });
                }

                Product? productToBeDeleted = _productRepository.GetFirstOrDefault(x => x.Id == id);
                if (productToBeDeleted == null)
                {
                    return StatusCode(404, new { success = false, error = "Product not found" });
                }

                string? existingImage = productToBeDeleted.ImageUrl;
                if (existingImage != null)
                {
                    _fileUploadService.RemoveImage(existingImage);
                }

                _productRepository.Remove(productToBeDeleted);
                _productRepository.Save();
                return Json(new { success = true, message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = $"Error while deleting product: {ex.Message}" });
            }
        }
        #endregion
    }
}
