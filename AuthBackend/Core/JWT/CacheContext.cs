using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using Messaging.Send.Sender;

namespace Core.JWT
{
	public class CacheContext : IMemoryCache
	{
		private readonly IDistributedCache _cache;
		private readonly ILoadCacheSender _loader;
		
		public CacheContext(IDistributedCache cache, ILoadCacheSender loader)
		{
			_cache = cache;
			_loader = loader;
		}
		
		private string GetKey<T>()
		{
			var tmp = (TableAttribute)typeof(T).GetCustomAttribute(typeof(TableAttribute));
			return tmp.Name;
		}
		
		private async Task<byte[]> GetEncodedCache(string key, int triesCount)
		{
			byte[] encodedChars = await _cache.GetAsync(key);
			
			if (encodedChars == null && triesCount < 1)
			{
				await _loader.CallAsync(new string[] { key });
				encodedChars = await GetEncodedCache(key, triesCount + 1);				
			}
			return encodedChars;			
		}

		public async Task<IEnumerable<T>> List<T>()
		{
			byte[] encodedChars = await GetEncodedCache(GetKey<T>(), 0);
			if (encodedChars == null)
			{
				Console.Error.WriteLine("La cache no esta disponible");
				return new List<T>();				
			}
			string buffer = Encoding.UTF8.GetString(encodedChars);
			var deserialized = JsonConvert.DeserializeObject<IEnumerable<T>>(buffer);
			if (deserialized == null)
			{
				return new List<T>();		
			}
			return deserialized;
		}
		
		public async Task<T> SingleOrDefaultAsync<T>(Func<T, bool> predicate)
		{
			var list = await List<T>();
			return list.SingleOrDefault(predicate);
		}
		
		public Task Store<T>(string key, T data)
		{
			if (data == null)
			{
				throw new Exception($"No es posible guardar un valor nulo en la llave {key}");
			}
			return _cache.SetStringAsync(key, JsonConvert.SerializeObject(data));	
		}

		public Task Store<T>(T data)
		{
			return _cache.SetStringAsync(GetKey<T>(), JsonConvert.SerializeObject(data));	
		}

		public Task StoreAll<T>(IEnumerable<T> data)
		{
			return _cache.SetStringAsync(GetKey<T>(), JsonConvert.SerializeObject(data));
		}

		public async Task<T> Get<T>()
		{
			byte[] encodedChars = await _cache.GetAsync(GetKey<T>());
			if (encodedChars == null)
			{
				return default(T);
			}
			string buffer = Encoding.UTF8.GetString(encodedChars);
			var deserialized = JsonConvert.DeserializeObject<T>(buffer);
			return deserialized;
		}

		public async Task<T> Get<T>(string key)
		{
			byte[] encodedChars = await _cache.GetAsync(key);
			if (encodedChars == null)
			{
				return default(T);
			}
			string buffer = Encoding.UTF8.GetString(encodedChars);
			var deserialized = JsonConvert.DeserializeObject<T>(buffer);
			return deserialized;
		}

		public Task Delete(string key)
		{																																																			            return _cache.RemoveAsync(key);
		}

		public Task Delete<T>()
		{
			return _cache.RemoveAsync(GetKey<T>());
		}
	}
}
