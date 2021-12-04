using System.Threading.Tasks;
using Core.Messaging.Send.Models;
using Core.Messaging.Send.Options;

namespace Messaging.Send.Sender
{
    public class LoadCacheSender: BaseSender, ILoadCacheSender 
    {
        public LoadCacheSender(IRabbitConfigurationProvider options): base("_LoadCache", options)
        {}

        public async Task<bool> CallAsync(string[] keys)
        {
            var parts = keys[0].Split("_");
            _queueName = parts[0] + "_LoadCache";

            var response = await ExecuteAsync<object>(new LoadCacheModel 
            {
                Keys = keys
            });

            return response.Status;
        }
    }
}
