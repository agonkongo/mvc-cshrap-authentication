using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace test_color_admin
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		protected void Application_EndRequest(object sender, EventArgs e) {
			HttpApplication application = (HttpApplication)sender;
			HttpContext context = application.Context;
			int _statusCode = application.Context.Response.StatusCode;
			bool isAjaxRequest = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
			if (isAjaxRequest && _statusCode == 401)
			{
				context.Response.Clear();
				context.Response.StatusCode = 409;
				context.Response.ContentType = "application/json";
				context.Response.Write("{ \"success\": false, \"message\": \"Please Logout.\" }");
				context.Response.End();
			}
			else if(_statusCode == 401)
			{
				// Redirect non-AJAX requests to the logout page
				context.Response.Clear();
				context.Response.Redirect("/login");
			}
		}
		protected void Application_PostAuthenticateRequest(Object sender, EventArgs e) {
			var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

			if (authCookie != null) {
				FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
				if (authTicket != null && !authTicket.Expired) {
					var roles = authTicket.UserData.Split(',');
					HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
				}
			}
		}
	}
}
