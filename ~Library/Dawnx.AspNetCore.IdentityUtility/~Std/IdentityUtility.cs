namespace Dawnx.AspNetCore.IdentityUtility
{
    public static class IdentityUtility
    {
        public static IdentityAuthority Authority;
    }

    public class IdentityAuthority
    {
        public Authority UserManager { get; set; }
        public Authority RoleManager { get; set; }
    }

}
