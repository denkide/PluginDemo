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
    public class PlivoService : IMessage
    {
        public SMSResponse SendMessage(string recipientNumber, string message)
        {
            return new SMSResponse
            {
                DiagnosticInformation = "",
                ErrorMessage = "In Plivo Service",
                ErrorFlag = false,
                ErrorID = 0
            }; ;
        }
    }
}
