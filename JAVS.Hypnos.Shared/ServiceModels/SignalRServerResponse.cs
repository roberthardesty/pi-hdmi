using System;
using System.Collections.Generic;
using System.Text;

namespace JAVS.Hypnos.Shared.ServiceModels
{
    public class SignalRServerResponse
    {
        public bool Success { get; set; }
        public Exception Error { get; set; }
    }
}
