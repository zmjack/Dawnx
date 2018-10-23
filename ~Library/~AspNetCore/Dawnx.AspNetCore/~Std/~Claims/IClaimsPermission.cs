using System.Linq;

namespace Dawnx.AspNetCore
{
    public interface IClaimsPermission
    {
        string Users { get; }
        string Roles { get; }
    }

    public static class DawnIClaimsPermission
    {
        public static string[] GetUsers(this IClaimsPermission @this)
            => @this.Users?.Split(',').Select(user => user.Trim()).ToArray() ?? new string[0];

        public static string[] GetRoles(this IClaimsPermission @this)
            => @this.Roles?.Split(',').Select(user => user.Trim()).ToArray() ?? new string[0];
    }

}
