using System;
using System.Linq;
using System.Net;

namespace Dawnx.Utilities
{
    public class IPUtility
    {
        /// <summary>
        /// Converts a <see cref="IPAddress"/>  into a long IP string.
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static long GetLongString(IPAddress ip)
            => BitConverter.ToUInt32(ip.GetAddressBytes().Reverse().ToArray(), 0);
        
    }
}
