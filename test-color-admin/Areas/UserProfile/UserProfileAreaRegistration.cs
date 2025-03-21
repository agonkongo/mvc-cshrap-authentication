﻿using System.Web.Mvc;

namespace test_color_admin.Areas.UserProfile
{
    public class UserProfileAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserProfile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UserProfile_default",
                "UserProfile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}