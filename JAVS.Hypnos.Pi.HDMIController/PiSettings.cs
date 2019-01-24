using System;
using System.Collections.Generic;
using System.Text;

namespace JAVS.Hypnos.Pi.HDMIController
{
    public class PiSettings: Core.PiSettings
    {
        public override string PiType { get => base.PiType ?? "HDMIController"; set => base.PiType = value; }
    }
}
