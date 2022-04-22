using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();  
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken] 

        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","The DisplayOrder cannot exactly match the name");
            }

            if (ModelState.IsValid)
            {

           
              _db.Categories.Add(obj);
              _db.SaveChanges();
              return RedirectToAction("Index");

            }

            return View(obj);
        }
    }
}
