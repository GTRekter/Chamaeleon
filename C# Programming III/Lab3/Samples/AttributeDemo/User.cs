using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AttributeDemo
{
	[Flags]
	public enum UserTypes
	{
		Unknown = 0,
		Guest = 1,
		PowerUser = 2,
		Administrator = 4
	}

	public class User
	{
		#region Properties
		/// <summary>
		/// The singleton User object used by the entire project
		/// </summary>
		public static User CurrentUser { get; set; }
		public UserTypes UserType { get; set; }
		public string UserName { get; set; }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// User cannot be created externally, must use a factory method such as LoginUser
		/// </summary>
		private User()
		{
		}
		#endregion Constructors

		/// <summary>
		/// Loads/logs in a user
		/// </summary>
		/// <param name="userType">User type</param>
		/// <param name="userName">User name</param>
		/// <remarks>
		/// In a real system, this method would likely be a true login method
		/// where the user provides a username and password and the database validates
		/// the user and returns teh access level that user has.
		/// </remarks>
		public static User LoginUser(UserTypes userType, string userName)
		{
			CurrentUser = new User() { UserType = userType, UserName = userName };
			return CurrentUser;
		}

		/// <summary>
		/// Validates that the user has the necessary permissions to execute the given method
		/// </summary>
		/// <param name="method">Method to check if user can execute</param>
		public static void ValidateUser(MethodInfo method)
		{
			CustomSecurityAttribute[] attr = (CustomSecurityAttribute[])method.GetCustomAttributes(
				typeof(CustomSecurityAttribute), true);
			if (attr != null && attr.Length > 0)
			{
				// Method contains CustomSecurityAttribute
				if (!attr[0].RequiredUserType.HasFlag(CurrentUser.UserType))
				{
					throw new InvalidOperationException("User does not have permission to execute " + method.Name);
				}
			}
		}
	}
}
