using System;

namespace SipWrapper
{
    public class LineInfo
    {
        public LineInfo(int id)
        {
            m_id = id;
            m_bCalling = false;
            m_bCallEstablished = false;
            m_bCallHeld = false;
            m_bCallPlayStarted = false;
            m_usrInputStr = "";
        }

        public int m_id;
        public int m_lastConnId;
        public bool m_bCalling;
        public bool m_bCallEstablished;
        public bool m_bCallHeld;
        public bool m_bCallPlayStarted;
        public string m_usrInputStr;
        public TimeSpan m_callTime;
        public string m_callTimeStr;
    }
}
