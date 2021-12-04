using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Messaging.Send.Options;

namespace Messaging.Send.Sender
{
    public class DispatchEmailSender: BaseSender, IDispatchEmailSender 
    {
       public DispatchEmailSender(IRabbitConfigurationProvider options): base("sys_DispatchEmail", options)
        {}
       
        public async Task<bool> CallAsync(string sender,List<string> remittents, string subject, string contentmesssage)
        {
            var response = await ExecuteAsync<string>(new  
            {
                Sender = sender,
                Remittents = remittents,
                Subject = subject,
                Message = contentmesssage
            });

            return response.Status;
        }
    }
}
