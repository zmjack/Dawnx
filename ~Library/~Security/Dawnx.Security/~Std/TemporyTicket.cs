using System;

namespace Dawnx.Security
{
    public class TemporyTicket
    {
        public DateTime SignTime { get; private set; }
        public DateTime AesKey { get; private set; }


        public TemporyTicket()
        {

        }

    }
}
