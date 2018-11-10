using System;
using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore.LiveAccountUtility
{
    public static class LiveAccountUtility
    {
        public static LiveAccountAuthority Authority;
    }

    public class LiveAccountAuthority
    {
        public Authority User { get; set; }
        public Authority Advanced { get; set; }
    }

}
