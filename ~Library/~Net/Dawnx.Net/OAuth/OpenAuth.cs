using Dawnx;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Net.OAuth
{
    public class OpenAuth_ClientCredentials : IOpenAuth
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string[] ApiScopes { get; set; }

        public string GrantType => "client_credentials";
        public string Authorization => $"{ClientId}:{ClientSecret}".Base64Encode();
        public Dictionary<string, object> RequestBody => new Dictionary<string, object>
        {
            ["grant_type"] = GrantType,
            ["scope"] = ApiScopes?.Join(" "),
        };
    }

    public class OpenAuth_AuthorizationCode : IOpenAuth
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string Code { get; set; }
        public string RedirectUri { get; set; }

        public string GrantType => "authorization_code";
        public string Authorization => $"{ClientId}:{ClientSecret}".Base64Encode();
        public Dictionary<string, object> RequestBody => new Dictionary<string, object>
        {
            ["grant_type"] = GrantType,
            ["code"] = Code,
            ["redirect_uri"] = RedirectUri,
        };
    }

    public class OpenAuth_Password : IOpenAuth
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string[] ApiScopes { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string GrantType => "password";
        public string Authorization => $"{ClientId}:{ClientSecret}".Base64Encode();
        public Dictionary<string, object> RequestBody => new Dictionary<string, object>
        {
            ["grant_type"] = GrantType,
            ["username"] = UserName,
            ["password"] = Password,
            ["scope"] = ApiScopes?.Join(" "),
        };
    }

}
