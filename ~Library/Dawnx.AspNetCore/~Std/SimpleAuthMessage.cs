using System;

namespace Dawnx.AspNetCore
{
    [Obsolete("Use TemporaryTicket to instead.")]
    public class SimpleAuthMessage
    {
        public DateTime Expire { get; set; }
        public string Username { get; set; }
    }
}