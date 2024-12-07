
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }


        /// <summary>
        /// This action is responsible for fetching all categories from the database and display them in the view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Category List";
            try
            {
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Error occur while fetching categories";
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// This action responsible for showing the form to create a new category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create Category";
            return View();
        }



        /// <summary>
        /// This is a post action to create a new category
        /// </summary>
        /// <param name="category">The category details</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order can't be same");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.Add(category);
                    _categoryRepository.Save();
                    TempData["success"] = "Category created successfully";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["error"] = "Error occur while creating category";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }



        /// <summary>
        /// This action fetch the category from ID and gives it to view
        /// </summary>
        /// <param name="id">The ID(Guid) of the category</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Update(int? id)
        {
            ViewData["Title"] = "Update Category";
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Category? category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    TempData["error"] = "The category you are looking for isn't found!";
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception)
            {
                TempData["error"] = "Error occur while fetching category";
                return RedirectToAction("Index");
            }
        }



        /// <summary>
        /// This action is responsible for updating the category
        /// </summary>
        /// <param name="category">The category details to be updated</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryRepository.Update(category);
                    _categoryRepository.Save();
                    TempData["success"] = "Category updated successfully";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Error occur while updating category";
                return RedirectToAction("Index");
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<Category> categories = _categoryRepository.GetAll().ToList();
                return StatusCode(200, new { data = categories });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error while retrieving categories: {ex.Message}" });
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return StatusCode(400, new { success = false, error = "Invalid category ID" });
                }

                Category? categoryToBeDeleted = _categoryRepository.GetFirstOrDefault(x => x.Id == id);
                if (categoryToBeDeleted == null)
                {
                    return StatusCode(404, new { success = false, error = "Product not found" });
                }

                _categoryRepository.Remove(categoryToBeDeleted);
                _categoryRepository.Save();
                return Json(new { success = true, message = "Category deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = $"Error while deleting category: {ex.Message}" });
            }
        }
        #endregion

    }
}
