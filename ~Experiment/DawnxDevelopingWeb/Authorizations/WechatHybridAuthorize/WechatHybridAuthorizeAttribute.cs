using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace DawnxTemplate.Authorizations.WechatHybridAuthorize
{
    public class WechatHybridAuthorizeAttribute : AuthorizeBaseAttribute
    {
        public override string PolicyPrefix => nameof(WechatHybridAuthorize);

        public WechatHybridAuthorizeAttribute(string authenticationType = null)
            : base(authenticationType)
        {
            Policy = $"{PolicyPrefix}";

            if (authenticationType != null)
                Policy += $"--type {AuthenticationType}";
        }

    }
}