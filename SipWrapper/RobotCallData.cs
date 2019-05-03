using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipWrapper
{
    public class RobotCallData
    {
        public string Status { get; internal set; }
        public bool Successful { get; internal set; }
        public TimeSpan CallDuration { get; internal set; }
    }
}
