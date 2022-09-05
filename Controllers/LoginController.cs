using OnRampAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnRampAssessment.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult GetLoginTypes()
        {
            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var loginTypesList = _context.tblLoginTypes.Select(l => new LoginTypeModel
                {
                  ID = l.roleID,
                  Type = l.Type
                }).ToList();

                return Json(loginTypesList, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult RegisterUser(string Username, string Password, int userType)
        //{
        //    byte[] encData_byte = new byte[Password.Length];
        //    encData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
        //    string encodedPassword = Convert.ToBase64String(encData_byte);

        //    using (OnRampDBEntities _context = new OnRampDBEntities())
        //    {
        //        var login = new tblLogin
        //        {
        //            Username = Username,
        //            Password = encodedPassword,
        //            roleID = userType,
        //            DateRegistered = DateTime.Now,
        //        };

        //        _context.tblLogins.Add(login);
        //        _context.SaveChanges();

        //        return Json(login, JsonRequestBehavior.AllowGet);
        //    }
        //}        
        public JsonResult LoginFuncion(string Username, string Password, int loginType)
        {
                                                           
            byte[] encData_byte = new byte[Password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
            string encodedPassword = Convert.ToBase64String(encData_byte);

            using (OnRampDBEntities _context = new OnRampDBEntities())
            {
                var loginSucess = _context.tblLogins.Where(a => a.roleID == loginType && a.Username.Equals(Username) && a.Password.Equals(encodedPassword)).Select(p => new LoginModel
                {
                    ID = p.ID,
                    UserName = p.Username,
                    login_type = new LoginTypeModel
                    {
                        ID = p.tblLoginType.roleID,
                        Type = p.tblLoginType.Type
                    }

                }).FirstOrDefault();

                if (loginSucess != null)
                {

                    Session["username"] = loginSucess.UserName;
                    Session["loginID"] = loginSucess.ID;
                    Session["LoginType"] = loginSucess.login_type.Type;
                    Session["LoginTypeID"] = loginSucess.login_type.ID;
                }

                return Json(loginSucess, JsonRequestBehavior.AllowGet);
            }
        }
    }
}