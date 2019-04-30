using SMSApi;
using System;
using System.Composition;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using SMSModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondService
{
    [Export(typeof(IMessage))]
    public class SecondService : IMessage, IDisposable
    {
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

        public Task<SMSResponse> SendMessageAsync(IDictionary<string, string> inputParms)
        {
            try
            {
                SMSResponse resp = new SMSResponse
                {
                    DiagnosticInformation = "",
                    ErrorMessage = "In Second Service",
                    ErrorFlag = false,
                    ErrorID = 0
                };

                var response = new TaskCompletionSource<SMSResponse>();
                response.SetResult(resp);

                return response.Task;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { Dispose(); }
        }
    }
}
