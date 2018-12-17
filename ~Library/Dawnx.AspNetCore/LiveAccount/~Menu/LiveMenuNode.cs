#if FINISH
namespace Dawnx.AspNetCore.LiveAccount
{
    public class LiveMenuNode : IClaimsPermission, INameable
    {
        public string IconClass { get; set; }
        public string Url { get; set; }

        public string Name { get; set; }
        public string Users { get; set; }
        public string Roles { get; set; }
    }
}
#endif