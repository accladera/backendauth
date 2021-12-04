using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using Core.JWT.Models;

namespace Core.JWT
{
		public class Session : ISession 
		{
        private readonly IMemoryCache _cache;
        private UserSession _session;

				public Session(IMemoryCache cache)
				{
						_cache = cache;
				}

        public async Task<bool> LoadData(string token)
        {
            var key = token.Split(" ")[1];
            _session = await _cache.Get<UserSession>($"jwt_{key}");
            return _session != null;
        }

        public Dictionary<string, object> GetData()
        {
            return _session.Data;
        }

        public int GetUserCode()
        {
            return _session.UserId;
        }

        public bool HasPermission(string[] transactions)
        {
            // TODO Cuando se complete la parte de transacciones agregar heuristica
            return true;
        }

        public bool IsAdmin()
        {
            return _session.IsAdmin;
        }

		public bool HasUser()
        {
            return _session != null;
        }
    }
}

