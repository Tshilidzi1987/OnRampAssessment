using OnRampAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnRampAssessment.Controllers
{
    public class CustomerOrderController : Controller
    {
        // GET: Order
        public ActionResult CustomerOrder()
        {
            return View();
        }
        public JsonResult OrderedProduct(int loginID)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {

                var orderList = _context.tblOrders.Where(l => l.tblCustomer.LoginID == loginID).Select(o => new OrderModel
                {
                    OrderID = o.OrderID,
                    Barcode = o.Barcode,
                    OrderName = o.ProductName,
                    Customer = new CustomerModel
                    {
                        CustomerID = o.tblCustomer.CustomerID,
                        Name = o.tblCustomer.Name,
                        Address = o.tblCustomer.Address,
                        Email = o.tblCustomer.Email,
                        Tel = o.tblCustomer.Tel
                    },
                    Products = new ProductsModel
                    {
                        Barcode = o.tblProduct.Barcode,
                        ProductName = o.tblProduct.Name,
                        Location = o.tblProduct.Location,
                        Warranty = o.tblProduct.Warranty,
                        DateCaptured = o.tblProduct.DateCaptured.ToString()
                    },
                    Supplier = new SupplierModel
                    {
                        SupplierID = o.tblProduct.tblSupplier.SupplierID,
                        Name = o.tblProduct.tblSupplier.Name,
                        Address = o.tblProduct.tblSupplier.Address,
                        Email = o.tblProduct.tblSupplier.Email,
                        Tel = o.tblProduct.tblSupplier.Tel
                    },
                    ProductCategory = new ProductCategoryModel
                    {
                        ProductCategoryID = o.tblProduct.tblProductCategory.CategoryID,
                        CategoryName = o.tblProduct.tblProductCategory.Name,
                        CategoryDescription = o.tblProduct.tblProductCategory.Description,
                        CategoryNameAndDesc = o.tblProduct.tblProductCategory.Name + " (" + o.tblProduct.tblProductCategory.Description + ")"
                    },
                    ProductStatus = new ProductStatusModel
                    {
                        ProductStatusID = o.tblProduct.tblProductStatu.ProductStatusID,
                        StatusDescription = o.tblProduct.tblProductStatu.Description
                    },
                    PaymentStatus = new PaymentStatusModel
                    {
                        paymentStatusID = o.tblPaymentStatu.PaymentStatusID,
                        paymentStatusDescription = o.tblPaymentStatu.Description
                    }
                }).OrderByDescending(o => o.OrderID).ToList();

                return Json(orderList, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult NewOrder(int customerID, string barcode, string productname, int supplierID)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var order = new tblOrder
                {
                    CustomerID = customerID,
                    Barcode = barcode,
                    ProductName = productname,
                    SupplierID = supplierID,
                    DateCaptured = DateTime.Now,
                    PaymentStatusID = 2,
                };
                _context.tblOrders.Add(order);

                var updateProduct = _context.tblProducts.Where(c => c.Barcode == barcode).FirstOrDefault();
                updateProduct.ProductStatusID = 5;
                _context.SaveChanges();                

                return Json(order, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult CancelOrder(string barcode)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var updateProduct = _context.tblProducts.Where(c => c.Barcode == barcode).FirstOrDefault();
                updateProduct.ProductStatusID = 1;
                
                var order = _context.tblOrders.Where(c => c.Barcode == barcode).FirstOrDefault();                
                _context.tblOrders.Remove(order);

                _context.SaveChanges();

                return Json(order, JsonRequestBehavior.AllowGet);
            }
        }
    }
}