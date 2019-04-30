using Microsoft.AspNetCore.Mvc;
using SMSModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSService_Demo2.Interfaces
{
    public interface ISMSProvider
    {
        Task<IEnumerable<(string recipientNumber, string message)>> SendMessageForAllServices();
        //SMSApi.SMSResponse SendSingleMessage<T>(T recipientNumber, T message, T service);

        Task<ActionResult<SMSResponse>> SendSingleMessage<T>(T rawRequest);
    }
}
