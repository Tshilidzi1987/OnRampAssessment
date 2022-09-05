using OnRampAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnRampAssessment.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Category()
        {
            return View();
        }
        public JsonResult GetCategory()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var customerList = _context.tblProductCategories.Select(c => new ProductCategoryModel
                {
                    ProductCategoryID = c.CategoryID,
                    CategoryName = c.Name,
                    CategoryDescription = c.Description                    
                }).ToList();
                return Json(customerList, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddCategory(string name, string description)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var category = new tblProductCategory
                {
                    Name = name,
                    Description = description                  
                };

                _context.tblProductCategories.Add(category);
                _context.SaveChanges();

                return Json(category, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteCategory(int categoryID)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {

                var category = _context.tblProductCategories.Where(c => c.CategoryID == categoryID).FirstOrDefault();
                _context.tblProductCategories.Remove(category);
                _context.SaveChanges();

                return Json(category, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditCategory(int categoryID, string name, string description)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {

                var category = _context.tblProductCategories.Where(c => c.CategoryID == categoryID).FirstOrDefault();
                category.Name = name;
                category.Description = description;              
                _context.SaveChanges();

                return Json(category, JsonRequestBehavior.AllowGet);
            }
        }
    }
}