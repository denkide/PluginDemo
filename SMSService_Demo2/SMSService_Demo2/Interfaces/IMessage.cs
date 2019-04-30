using SMSService_Demo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSService_Demo2.Interfaces
{
    public interface IMessage
    {
        SMSResponse SendMessage(string recipientNumber, string message);
    }
}
