using System;

namespace Dawnx.AspNetCore
{
    [Obsolete("Use TemporyTicket to instead.")]
    public class SimpleAuthMessage
    {
        public DateTime Expire { get; set; }
        public string Username { get; set; }
    }
}