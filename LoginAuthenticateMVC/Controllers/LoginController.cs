using LoginAuthenticateMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginAuthenticateMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(LoginAuthenticateMVC.Models.tblUser userModel)
        {
            using (dbMVCEntities db=new dbMVCEntities())
            {
                var userDetails = db.tblUsers.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "wrong username or password!!";
                    return View("Index",userModel);
                }
                else
                {
                    Session["UserId"] = userModel.UserId;
                    Session["UserName"] = userModel.UserName;
                    return RedirectToAction("Index","Home");
                }
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
    }
}