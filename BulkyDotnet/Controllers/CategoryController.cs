using BulkyDotnet.Data;
using BulkyDotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace BulkyDotnet.Controllers
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
           List<Category> objCategoryList= _db.Categories.ToList();

           return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        } 

       [HttpPost]
        public IActionResult Create(Category obj)
         {
            if (obj.Name == obj.DisplayOrder.ToString())
            {     //custom validation
                ModelState.AddModelError("name", "the display order cannot match the name");
            }

            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("","Test is an invalid value");
            }

            if (ModelState.IsValid) 
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();                         
        }
        public IActionResult Edit(int? id)
        {
            if(id==null|| id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb=_db.Categories.Find(id);//only works with primary key
           /* Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);//if record doesn't found it will return null object
            Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id== id).FirstOrDefault();*/
                                  
            if (categoryFromDb == null)
            {
                return NotFound();    

            }
            
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);//only works with primary key
            /* Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);//if record doesn't found it will return null object
             Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id== id).FirstOrDefault();*/

            if (categoryFromDb == null)
            {
                return NotFound();

            }

            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "category deleted successfully";
            return RedirectToAction("Index", "Category");
            
            
        }
    }
}
