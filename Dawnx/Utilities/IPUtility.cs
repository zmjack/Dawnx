using System;
using System.Linq;
using System.Net;

namespace Dawnx.Utilities
{
    public class IPUtility
    {
        /// <summary>
        /// Converts a <see cref="IPAddress"/>  into a long value.
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static long GetLongValue(IPAddress ip)
            => BitConverter.ToUInt32(ip.GetAddressBytes().Reverse().ToArray(), 0);

        /// <summary>
        /// Converts a IP value into a <see cref="IPAddress"/>.
        /// </summary>
        /// <param name="ipValue"></param>
        /// <returns></returns>
        public static IPAddress CreateFromLongValue(long ipValue)
            => new IPAddress(BitConverter.GetBytes((uint)ipValue).Reverse().ToArray());

    }
}
