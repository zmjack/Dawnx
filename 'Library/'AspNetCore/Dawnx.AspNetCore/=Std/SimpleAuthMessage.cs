using System;

namespace Dawnx.AspNetCore
{
    public class SimpleAuthMessage
    {
        public DateTime Expire { get; set; }
        public string Username { get; set; }
    }
}