using OnRampAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnRampAssessment.Controllers
{
    public class AdminOrderController : Controller
    {
        // GET: AdminOrder
        public ActionResult Order()
        {
            return View();
        }
        public JsonResult OrderedProduct()
        {

            using (OnRampDBEntities _context = new OnRampDBEntities())
            {               

                var orderList = _context.tblOrders.Select(o => new OrderModel
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
                    Products = new ProductsModel { 
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
        public JsonResult PaymentStatuses()
        {

            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var paymentStatusesList = _context.tblPaymentStatus.Select(p => new PaymentStatusModel
                {
                    paymentStatusID = p.PaymentStatusID,
                    paymentStatusDescription = p.Description
                }).ToList();

                return Json(paymentStatusesList, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateOrderPaymentAndProductStatuses(int? productStatusID, int? paymentStatusID, string barcode)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var updateProduct = _context.tblProducts.Where(c => c.Barcode == barcode).FirstOrDefault();
                updateProduct.ProductStatusID = productStatusID;

                var order = _context.tblOrders.Where(c => c.Barcode == barcode).FirstOrDefault();
                order.PaymentStatusID = paymentStatusID;

                _context.SaveChanges();

                return Json(order, JsonRequestBehavior.AllowGet);
            }
        }
    }
}