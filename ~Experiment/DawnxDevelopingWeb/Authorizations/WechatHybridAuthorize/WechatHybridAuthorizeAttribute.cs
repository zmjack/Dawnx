using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace DawnxTemplate.Authorizations.WechatHybridAuthorize
{
    public class WechatHybridAuthorizeAttribute : SchemaAuthorizeAttribute
    {
        public override string PolicyPrefix => nameof(WechatHybridAuthorize);

        public WechatHybridAuthorizeAttribute(string schema = null)
            : base(schema)
        {
            Policy = $"{PolicyPrefix}";

            if (schema != null)
                Policy += $"--schema {AuthenticationScheme}";
        }

    }
}