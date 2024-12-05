
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
                List<Category> categories = _categoryRepository.GetAll().ToList();
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
                    Console.WriteLine($"Error occurred while fetching categories: {e.Message}");
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
            catch (Exception e)
            {
                TempData["error"] = "Error occur while fetching category";
                Console.WriteLine($"Error occurred while fetching categories: {e.Message}");
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
            catch (Exception e)
            {
                TempData["error"] = "Error occur while updating category";
                Console.WriteLine($"Error occurred while fetching categories: {e.Message}");
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// This action fetch the category from ID and gives it to view
        /// </summary>
        /// <param name="id">Yhe ID(Guid) of the category</param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            ViewData["Title"] = "Delete Category";
            try
            {
                Category? category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    TempData["error"] = "The category you are looking for isn't found!";
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception e)
            {
                TempData["error"] = "Error occur while fetching category";
                Console.WriteLine($"Error occurred while fetching categories: {e.Message}");
                return RedirectToAction("Index");
            }
        }



        /// <summary>
        /// This action gets the id of the category to be deleted and delete it from the database
        /// </summary>
        /// <param name="id">ID(Guid) of the category</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null)
            {
                return View();
            }
            try
            {
                Category? foundCat = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
                if (foundCat == null)
                {
                    TempData["error"] = "The category you are looking for isn't found!";
                    return RedirectToAction("Index");
                }
                _categoryRepository.Remove(foundCat);
                _categoryRepository.Save();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error occur while deleting category";
                Console.WriteLine($"Error occurred while fetching categories: {e.Message}");
                return RedirectToAction("Index");
            }
        }

    }
}
