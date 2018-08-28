using System;
using System.Collections.Generic;
using System.Text;

namespace JAVS.Hypnos.Shared.ServiceModels
{
    public class JoinGroupRequest
    {
        public bool IsDetector { get; set; }
        public bool IsHDMIController { get; set; }
        public string Password { get; set; }
    }
}
