using System;
using System.Net;

namespace Dawnx
{
    public static class DawnIPAddress
    {
        /// <summary>
        /// Converts a IP Address into a uint value.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToLong(this IPAddress @this)
            => BitConverter.ToUInt32(@this.GetAddressBytes(), 0);

    }
}
