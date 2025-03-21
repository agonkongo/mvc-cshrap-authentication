using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test_color_admin.Models
{
	public class UserModel
	{
		public int id { get; set; }
		public string Email { get; set; }
		public string Roles { get; set; }
		public string Password { get; set; }
	}
}