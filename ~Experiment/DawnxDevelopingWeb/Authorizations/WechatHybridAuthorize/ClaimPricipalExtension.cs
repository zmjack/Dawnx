using System;
using System.Security.Claims;
using Dawnx.AspNetCore;

namespace DawnxTemplate.Authorizations.WechatHybridAuthorize
{
    public static class ClaimPricipalExtension
    {
        public static WechatHybridUser GetWechatHybridUser(this ClaimsPrincipal @this, string schema = null)
        {
            return new WechatHybridUser
            {
                OpenIdType = Enum.Parse<WechatHybridOpenIdType>(@this.GetClaim(schema, $"Wechat{nameof(WechatHybridUser.OpenIdType)}")),
                OpenId = @this.GetClaim(schema, $"Wechat{nameof(WechatHybridUser.OpenId)}"),
                PubUserName = @this.GetClaim(schema, $"Wechat{nameof(WechatHybridUser.PubUserName)}"),
                EntUserName = @this.GetClaim(schema, $"Wechat{nameof(WechatHybridUser.EntUserName)}"),
            };
        }

    }
}
