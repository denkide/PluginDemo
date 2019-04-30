using Microsoft.AspNetCore.Mvc;
using SMSModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSApi
{
    public interface IMessage
    {
        Task<SMSResponse> SendMessageAsync(IDictionary<string, string> inputParms);
    }
}
