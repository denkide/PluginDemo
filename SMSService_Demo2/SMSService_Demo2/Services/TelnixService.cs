using SMSService_Demo2.Interfaces;
using SMSService_Demo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Composition;

namespace SMSService_Demo2.Services
{
    [Export(typeof(IMessage))]
    public class TelnixService : IMessage
    {
        public SMSResponse SendMessage(string recipientNumber, string message)
        {
            return new SMSResponse
            {
                DiagnosticInformation = "",
                ErrorMessage = "In Telnix Service",
                ErrorFlag = false,
                ErrorID = 0
            };
        }
    }
}
