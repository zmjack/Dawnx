using NStandard;
using System;
using System.Collections.Generic;

namespace Dawnx.Net.OAuth
{
    public static partial class OpenAuth
    {
        public class ClientCredentialsAuth : IOpenAuth
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string[] ApiScopes { get; set; }

            public string GrantType => "client_credentials";
            public string Authorization => $"{ClientId}:{ClientSecret}".Flow(StringFlows.Base64);
            public Dictionary<string, object> RequestBody => new Dictionary<string, object>
            {
                ["grant_type"] = GrantType,
                ["scope"] = ApiScopes?.Join(" "),
            };
        }
    }

}
