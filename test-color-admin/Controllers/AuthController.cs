using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;

using test_color_admin.Models;

namespace test_color_admin.Controllers
{
  public class AuthController : Controller
  {
    static List<UserModel> users = new List<UserModel>() {
      new UserModel() {
          id = 1, Email = "abc@gmail.com", Roles = "Admin,Editor", Password = "123"
        },
        new UserModel() {
          id = 2, Email = "aaa@gmail.com", Roles = "common-user", Password = "123"
        },
        new UserModel() {
          id = 3, Email = "xyz@gmail.com", Roles = "Editor", Password = "123"
        }
    };
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public ActionResult LoginAuth(UserModel user)
    {
      var x = verifyLogin(user);
      if (x != null)
      {
        string _userid = x.id.ToString();
        string role = ConfigurationManager.AppSettings["COMMON_USER"].ToString();
        int ticketExpired = int.Parse(ConfigurationManager.AppSettings["TICKET_EXPIRED"]);
        FormsAuthentication.SetAuthCookie(_userid, false);
        var authTicket = new FormsAuthenticationTicket(1, _userid, DateTime.Now, DateTime.Now.AddMinutes(ticketExpired), false, role);
        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        HttpContext.Response.Cookies.Add(authCookie);
        return Json(new
        {
          success = true,
          msg = "successfully login."
        }, JsonRequestBehavior.AllowGet);
      }
      else
      {
        return Json(new
        {
          success = false,
          msg = "user not found."
        }, JsonRequestBehavior.AllowGet);
      }

    }
    private static UserModel verifyLogin(UserModel user)
    {
      return users.Where(u => u.Email.ToLower() == user.Email.ToLower() &&
        u.Password == user.Password).FirstOrDefault();
    }
    public ActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public ActionResult LogOff()
    {
      FormsAuthentication.SignOut();
      return Redirect("/");
    }
    public ActionResult Forgotpassword()
    {
      return View();
    }
  }
}