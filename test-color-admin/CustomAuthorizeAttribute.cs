using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
  protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
  {
    if (filterContext.HttpContext.Request.IsAjaxRequest())
    {
      filterContext.HttpContext.Response.StatusCode = 401;
      filterContext.Result = new JsonResult
      {
        Data = new { success = false, message = "You are not authorized to access this resource." },
        JsonRequestBehavior = JsonRequestBehavior.AllowGet
      };
    }
    else
    {
      filterContext.HttpContext.Response.Redirect("/");
    }
  }
}
