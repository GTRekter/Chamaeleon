using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AttributeDemo
{
	public class ProcessData
	{
		/// <summary>
		/// Load data requires that the user be a Guest, PowerUser or Admin
		/// </summary>
		/// <returns></returns>
		[CustomSecurity(RequiredUserType = UserTypes.Guest | UserTypes.PowerUser | UserTypes.Administrator)]
		public object LoadData()
		{
			User.ValidateUser(System.Reflection.MethodBase.GetCurrentMethod() as MethodInfo);
			// Passed security
			return null;
		}

		/// <summary>
		/// Save data requires that the user be a PowerUser or Admin
		/// </summary>
		[CustomSecurity(RequiredUserType = UserTypes.PowerUser | UserTypes.Administrator)]
		public void SaveData(object data)
		{
			User.ValidateUser(System.Reflection.MethodBase.GetCurrentMethod() as MethodInfo);
			// Passed security
		}

		/// <summary>
		/// Delete data is only available for an Admin
		/// </summary>
		[CustomSecurity(RequiredUserType = UserTypes.Administrator)]
		public void DeleteData(object data)
		{
			User.ValidateUser(System.Reflection.MethodBase.GetCurrentMethod() as MethodInfo);
			// Passed security
		}
	}
}
