using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.Send.Sender
{
    public interface IDispatchEmailSender 
    {
        Task<bool> CallAsync( string sender,List<string> remittents, string subject, string content);
    }
} 
