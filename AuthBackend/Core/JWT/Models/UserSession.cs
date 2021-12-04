using System.Collections.Generic;

namespace Core.JWT.Models 
{
		public class UserSession
		{
				public bool IsAdmin { get; set; }
				public Dictionary<string, object> Data { get; set; }
				public int UserId { get; set; }
		}
}
