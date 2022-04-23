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
              TempData["success"] = "Category created successfully";
              return RedirectToAction("Index");

            }

            return View(obj);
        }   

        //GET 

        public IActionResult Delete(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }

            var category = _db.Categories.Find(id); 

            if(category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully"; 
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id ==0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);   

            if(categoryFromDb == null)
            {
                return NotFound();
            }

           // var categoryFromDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);  


            return View(categoryFromDb);
        }

        // POST
        [HttpPost] 
        [ValidateAntiForgeryToken] 

        public IActionResult Edit(Category obj)
        {   

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the name");
            }

            if(ModelState.IsValid)
            {
               _db.Categories.Update(obj);
               _db.SaveChanges();
               TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");

            }
            return View(obj); 
        }

        
    }
}
