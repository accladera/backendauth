using System.Threading.Tasks;

namespace Messaging.Send.Sender
{
    public interface ILoadCacheSender
    {
        Task<bool> CallAsync(string[] keys);
    }
} 
