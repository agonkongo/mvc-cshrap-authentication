using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test_color_admin.Areas.UserProfile.Controllers.UserProfile
{
  [Authorize]
  public class UserProfileController : Controller
  {
    [CustomAuthorize(Roles = "common-user")]
    public ActionResult Index()
    {
      int userId = Convert.ToInt32(User.Identity.Name);
      return Json(new
      {
        success = true,
        userId = userId
      }, JsonRequestBehavior.AllowGet);
    }
  }
}