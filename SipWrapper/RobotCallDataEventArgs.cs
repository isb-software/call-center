﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipWrapper
{
    public class RobotCallDataEventArgs : EventArgs
    {
        public bool HasTimedOut { get; internal set; }
        public string Status { get; internal set; }
        public bool Successful { get; internal set; }
        public TimeSpan TotalCallDuration { get; internal set; }
        public TimeSpan ConversationDuration { get; internal set; }
        public string PhoneNumber { get; internal set; }

    }
}
