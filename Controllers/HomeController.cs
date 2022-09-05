using OnRampAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnRampAssessment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DeleteProduct(string barcode)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var product = _context.tblProducts.Where(c => c.Barcode == barcode).FirstOrDefault();
                _context.tblProducts.Remove(product);
                _context.SaveChanges();

                return Json(product, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EditProduct(int? supplierID, int? categoryID, int? productStatusID, string barcode, string productName, bool? warranty, string location)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {

                var product = _context.tblProducts.Where(c => c.Barcode == barcode).FirstOrDefault();
                product.SupplierID = supplierID;
                product.ProductCategoryID = categoryID;
                product.ProductStatusID = productStatusID;
                product.Name = productName;
                product.Warranty = warranty;
                product.Location = location;
                _context.SaveChanges();

                return Json(product, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddNewProduct(string barcode, string productName, int? supplierID, int? categoryID, int? productStatusID, bool? warranty, string location)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {

                var product = new tblProduct { 
                    Barcode = barcode,
                    Name = productName,
                    SupplierID = supplierID,
                    ProductCategoryID = categoryID,
                    ProductStatusID = productStatusID,
                    Warranty = warranty,
                    Location = location,
                    DateCaptured = DateTime.UtcNow

                };
                _context.tblProducts.Add(product);
                _context.SaveChanges();

                return Json(product, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetProducts()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var productList = _context.tblProducts.Where(a => a.ProductStatusID == 1).Select(p => new ProductsModel
                {
                    Barcode = p.Barcode,
                    ProductName = p.Name,
                    DateCaptured = p.DateCaptured.ToString(),
                    Location = p.Location,
                    Warranty = p.Warranty,
                    ProductCategory = new ProductCategoryModel
                    {
                        ProductCategoryID = p.tblProductCategory.CategoryID,
                        CategoryName = p.tblProductCategory.Name,
                        CategoryDescription = p.tblProductCategory.Description,
                        CategoryNameAndDesc = p.tblProductCategory.Name + " (" + p.tblProductCategory.Description + ")"
                    },
                    ProductStatus = new ProductStatusModel
                    {
                        ProductStatusID = p.tblProductStatu.ProductStatusID,
                        StatusDescription = p.tblProductStatu.Description
                    },
                    Supplier = new SupplierModel
                    {
                        SupplierID = p.tblSupplier.SupplierID,
                        Email = p.tblSupplier.Email,
                        Name = p.tblSupplier.Name,
                        Tel = p.tblSupplier.Tel
                    }
                }).ToList();

                return Json(productList, JsonRequestBehavior.AllowGet);
            }            
                      
        }
        public JsonResult GetSuppliers()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var SupplierList = _context.tblSuppliers.Select(p => new SupplierModel
                {
                    SupplierID = p.SupplierID,
                    Name = p.Name,
                    Email = p.Email,
                    Tel = p.Tel
                }).ToList();
                return Json(SupplierList, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetProductCatergories()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var ProductCategoryList = _context.tblProductCategories.Select(p => new ProductCategoryModel { 
                    ProductCategoryID = p.CategoryID,
                    CategoryName = p.Name,
                    CategoryNameAndDesc = p.Name + " - (" + p.Description + ")",
                    CategoryDescription = p.Description               
                }).ToList();
                return Json(ProductCategoryList, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetProductStatuses()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var ProductStatusList = _context.tblProductStatus.Select(p => new ProductStatusModel
                {
                   ProductStatusID = p.ProductStatusID,
                   StatusDescription = p.Description
                }).ToList();
                return Json(ProductStatusList, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ProductInStockChart()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {               
                var productList = _context.tblProducts.Where(a => a.ProductStatusID == 1).GroupBy(b => b.tblProductCategory.Name).Select(p => new ProductPie
                {
                  ProductCategory = p.Key,
                  Count = p.Select(c => c.tblProductCategory.Name).Count()
                   
                }).ToList();

                return Json(productList, JsonRequestBehavior.AllowGet);
            }
        }       
        public JsonResult OrdersWithOutstandingPaymentsChart()
        {          

            using (OnRampDBEntities _context = new OnRampDBEntities())
            {              

                var results = _context.tblOrders.Where(o => o.PaymentStatusID == 2).GroupBy(a => a.tblProduct.tblProductCategory.Name).Select(p => new ProductPie
                {
                    ProductCategory= p.Key,
                    Count = p.Select(c => c.tblProduct.tblProductCategory.Name).Count()
                }).ToList();
                return Json(results, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult MonthlyOrdersChart()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var results = _context.tblOrders.Select(p => new ProductPie
                {
                    OrderDate = p.DateCaptured,
                    ProductCategory = p.tblProduct.tblProductCategory.Name                    
                }).ToList();
                return Json(results.AsEnumerable().GroupBy(a => Convert.ToDateTime(a.OrderDate).ToString("yyyy-MM")).Select(r => new 
                {
                    OrderDate = r.Key,
                    Count = r.Count()
                    
                }), JsonRequestBehavior.AllowGet);
            }
        }
    }
}