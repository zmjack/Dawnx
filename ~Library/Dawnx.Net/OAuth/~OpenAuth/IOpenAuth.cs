using System.Collections.Generic;

namespace Dawnx.Net.OAuth
{
    public interface IOpenAuth
    {
        string GrantType { get; }
        string Authorization { get; }
        Dictionary<string, object> RequestBody { get; }
    }
}
