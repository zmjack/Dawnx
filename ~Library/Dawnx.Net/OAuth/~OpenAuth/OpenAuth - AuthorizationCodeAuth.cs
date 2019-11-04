using NStandard;
using NStandard.Flows;
using System.Collections.Generic;

namespace Dawnx.Net.OAuth
{
    public static partial class OpenAuth
    {
        public class AuthorizationCodeAuth : IOpenAuth
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }

            public string Code { get; set; }
            public string RedirectUri { get; set; }

            public string GrantType => "authorization_code";
            public string Authorization => $"{ClientId}:{ClientSecret}".Flow(StringFlow.Base64);
            public Dictionary<string, object> RequestBody => new Dictionary<string, object>
            {
                ["grant_type"] = GrantType,
                ["code"] = Code,
                ["redirect_uri"] = RedirectUri,
            };
        }
    }

}
