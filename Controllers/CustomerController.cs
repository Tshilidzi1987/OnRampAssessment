using OnRampAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnRampAssessment.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Customer()
        {
            return View();
        }

        public JsonResult GetCustomer()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var customerList = _context.tblCustomers.Select(c => new CustomerModel
                {
                    CustomerID = c.CustomerID,
                    Name = c.Name,
                    Email = c.Email,
                    Tel = c.Tel,
                    Address = c.Address
                }).ToList();
                return Json(customerList, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetCustomerByLoginID(int loginID)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var customerObj = _context.tblCustomers.Where(d => d.tblLogin.ID == loginID).Select(c => new CustomerModel
                {
                    CustomerID = c.CustomerID,
                    Name = c.Name,
                    Email = c.Email,
                    Tel = c.Tel,
                    Address = c.Address
                }).FirstOrDefault();
                return Json(customerObj, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddCustomer(string username, string password, string name, string email, string tel, string address)
        {
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedPassword = Convert.ToBase64String(encData_byte);

            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var login = new tblLogin
                {
                    Username = username,
                    Password = encodedPassword,
                    roleID = 1,
                    DateRegistered = DateTime.Now,
                };
                _context.tblLogins.Add(login);
              
                var customers = new tblCustomer { 
                    Name = name,
                    Email = email,
                    Tel = tel,
                    Address = address,
                    LoginID = login.ID
                };
                _context.tblCustomers.Add(customers);
                _context.SaveChanges();                

                return Json(customers, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteCustomer(int customerID)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                
                var customer = _context.tblCustomers.Where(c => c.CustomerID == customerID).FirstOrDefault();
                _context.tblCustomers.Remove(customer);
                _context.SaveChanges();

                return Json(customer, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditCustomer(int customerID, string name, string email, string tel, string address)
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {

                var customer = _context.tblCustomers.Where(c => c.CustomerID == customerID).FirstOrDefault();
                customer.Name = name;
                customer.Email = email;
                customer.Tel = tel;
                customer.Address = address;
                _context.SaveChanges();

                return Json(customer, JsonRequestBehavior.AllowGet);
            }
        }
    }
}