using Bulky_Web.Data;
using Bulky_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky_Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext db)
        {
            _dbContext = db;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Categories List";
            List<Category> categories = _dbContext.Categories.ToList();
            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Create Category";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order can't be same");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Edit Category";
            Category? category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Delete(int id) 
        {
            ViewData["Title"] = "Delete Category";
            Category? category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

    }
}
