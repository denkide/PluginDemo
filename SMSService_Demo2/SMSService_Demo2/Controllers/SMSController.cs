using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;
using SMSModels;
using SMSService_Demo2.Interfaces;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SMSService_Demo2.Controllers
{
    [ApiController]
    public class SMSController : ControllerBase, IDisposable 
    {
        private readonly ISMSProvider _sendMessageProvider;

        public SMSController(ISMSProvider sendMessageProvider)
        {
            _sendMessageProvider = sendMessageProvider;     
        }


        #region IDisposable Stuff

        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        #endregion

        [HttpPost]
        [Route("api/sms/send")]
        public async Task<IActionResult> SendMessage([FromBody] string rawQuery)
        {
                string inputRequest = rawQuery;
                var retVal = await _sendMessageProvider.SendSingleMessage(rawQuery);
                return Ok(retVal);

        }
    }
}