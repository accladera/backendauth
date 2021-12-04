using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Core.JWT
{
		public interface ISession
		{
				Task<bool> LoadData(string token);

				int GetUserCode();

				Dictionary<string, object> GetData();

				bool IsAdmin();

				bool HasPermission(string[] transactions);

				bool HasUser();
		}
}

