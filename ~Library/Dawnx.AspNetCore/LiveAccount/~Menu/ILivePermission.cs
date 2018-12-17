#if FINISH
using System.Linq;

namespace Dawnx.AspNetCore
{
    public interface ILivePermission
    {
        string LiveRoles { get; }
    }

    public static class DawnILivePermission
    {
        public static string[] GetLiveRoles(this ILivePermission @this)
            => new string[0];//@this.Roles?.Split(',').Select(user => user.Trim()).ToArray() ?? new string[0];
    }

}
#endif