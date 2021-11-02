using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttributeDemo
{
	[AttributeUsage(AttributeTargets.Method)]
	public class CustomSecurityAttribute : Attribute
	{
		public UserTypes RequiredUserType { get; set; }
	}
}
