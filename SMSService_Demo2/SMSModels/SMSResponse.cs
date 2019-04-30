using System;

namespace SMSModels
{
    public class SMSResponse
    {
        /// <summary>
        /// True if an error exists False if request completed successfully.
        /// </summary>
        public bool ErrorFlag { get; set; }

        /// <summary>
        /// Error message if ErrorFlag = Y, null if no error
        /// </summary>        
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Error message if ErrorFlag = Y, null if no error
        /// </summary>        
        public int ErrorID { get; set; }

        /// <summary>
        /// Any relevant diagnostic information for debugging
        /// </summary>        
        public string DiagnosticInformation { get; set; }
    }
}
