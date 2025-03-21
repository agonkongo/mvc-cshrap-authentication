using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace test_color_admin
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			var authRoutes = new[]
			{
					new { name = "Login", url = "login", action = "Login" },
					new { name = "LogOff", url = "logoff", action = "LogOff" },
					new { name = "Register", url = "register", action = "Register" },
					new { name = "LoginAuth", url = "loginauth", action = "LoginAuth" },
					new { name = "ForgotPassword", url = "forgot-password", action = "Forgotpassword" }
			};

			foreach (var route in authRoutes)
			{
				routes.MapRoute(
						name: route.name,
						url: route.url,
						defaults: new { controller = "Auth", action = route.action, id = UrlParameter.Optional }
				);
			}
			routes.MapRoute(
					name: "Default",
					url: "{controller}/{action}/{id}",
					defaults: new { controller = "Auth", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
