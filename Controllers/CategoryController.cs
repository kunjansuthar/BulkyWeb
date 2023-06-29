using BulkyWeb.Context;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            try
            {
                List<Category> objCategoryList = _db.categories.Where(x => x.Id != 0).ToList();

                if (ModelState.IsValid)
                {

                    return View(objCategoryList);
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            try
            {
                if (obj.Name != null && obj.DisplayOrder != 0 && obj.Id == 0)
                {
                    var Name = _db.categories.Where(x => x.Name.ToLower() == obj.Name.ToLower()).FirstOrDefault();
                    var DisplayOrder = _db.categories.Where(x => x.DisplayOrder.ToString() == obj.DisplayOrder.ToString()).FirstOrDefault();

                    if (Name != null || DisplayOrder != null)
                    {
                        if (Name != null)
                        {
                            ModelState.AddModelError("Name", "categoty name alredy exsiting.*");
                        }
                        else 
                        {
                            ModelState.AddModelError("DisplayOrder", "Display Oreder alredy exsiting.*");
                        }
                    }
                    else
                    {
                        _db.categories.Add(obj);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }  
                }
               
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }


        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }

                Category category = _db.categories.Find(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.categories.Update(obj);

                    var Name = _db.categories.Where(x => x.Name.ToLower() == obj.Name.ToLower()).FirstOrDefault();
                    var DisplayOrder = _db.categories.Where(x => x.DisplayOrder.ToString() == obj.DisplayOrder.ToString()).FirstOrDefault();

                    if (Name != null && Name.Id != obj.Id)
                    {
                        ModelState.AddModelError("Name", "categoty name alredy exsiting.*");
                    }
                    else if (DisplayOrder != null && DisplayOrder.Id != obj.Id)
                    {
                        ModelState.AddModelError("DisplayOrder", "Display Oreder alredy exsiting.*");
                    }
                    else
                    {
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            } 
        }
    }
}
