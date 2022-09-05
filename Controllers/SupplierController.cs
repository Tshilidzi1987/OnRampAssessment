using OnRampAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnRampAssessment.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Supplier()
        {
            return View();
        }
        public JsonResult GetSupplier()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var supplierList = _context.tblSuppliers.Select(c => new SupplierModel
                {
                    SupplierID = c.SupplierID,
                    Name = c.Name,
                    Email = c.Email,
                    Tel = c.Tel,
                    Address = c.Address
                }).ToList();
                return Json(supplierList, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddSupplier(string name, string email, string tel, string address)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var supplier = new tblSupplier
                {
                    Name = name,
                    Email = email,
                    Tel = tel,
                    Address = address
                };

                _context.tblSuppliers.Add(supplier);
                _context.SaveChanges();

                return Json(supplier, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteSupplier(int supplierID)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var supplier = _context.tblSuppliers.Where(c => c.SupplierID == supplierID).FirstOrDefault();
                _context.tblSuppliers.Remove(supplier);
                _context.SaveChanges();

                return Json(supplier, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditSupplier(int supplierID, string name, string email, string tel, string address)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {

                var supplier = _context.tblSuppliers.Where(c => c.SupplierID == supplierID).FirstOrDefault();
                supplier.Name = name;
                supplier.Email = email;
                supplier.Tel = tel;
                supplier.Address = address;
                _context.SaveChanges();

                return Json(supplier, JsonRequestBehavior.AllowGet);
            }
        }
    }
}