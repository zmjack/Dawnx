using NStandard;
using System;
using System.Collections.Generic;

namespace Dawnx.Net.OAuth
{
    public static partial class OpenAuth
    {
        public class PasswordAuth : IOpenAuth
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string[] ApiScopes { get; set; }

            public string UserName { get; set; }
            public string Password { get; set; }

            public string GrantType => "password";
            public string Authorization => $"{ClientId}:{ClientSecret}".Flow(StringFlows.FromBase64);
            public Dictionary<string, object> RequestBody => new Dictionary<string, object>
            {
                ["grant_type"] = GrantType,
                ["username"] = UserName,
                ["password"] = Password,
                ["scope"] = ApiScopes?.Join(" "),
            };
        }
    }

}
