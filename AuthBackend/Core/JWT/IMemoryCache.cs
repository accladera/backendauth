using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Core.JWT
{
		public interface IMemoryCache
		{
					Task<IEnumerable<T>> List<T>();

					Task Store<T>(string key, T data);

					Task Store<T>(T data);

					Task StoreAll<T>(IEnumerable<T> data);

					Task<T> Get<T>();

					Task<T> Get<T>(string key);

					Task Delete<T>();

					Task Delete(string key);

					Task<T> SingleOrDefaultAsync<T>(Func<T, bool> predicate);
																				
		}
		
}

