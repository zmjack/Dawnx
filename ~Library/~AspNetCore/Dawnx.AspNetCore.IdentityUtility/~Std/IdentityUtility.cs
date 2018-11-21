namespace Dawnx.AspNetCore.IdentityUtility
{
    public static class IdentityUtility
    {
        public static IdentityAuthority Authority;
    }

    public class IdentityAuthority
    {
        public Authority User { get; set; }
        public Authority Role { get; set; }
    }

}
