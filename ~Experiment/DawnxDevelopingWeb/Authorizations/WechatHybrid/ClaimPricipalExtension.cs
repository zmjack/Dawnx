using System;
using System.Security.Claims;
using Dawnx.AspNetCore;

namespace DawnxDevelopingWeb.Authorizations.WechatHybrid
{
    public static class ClaimPricipalExtension
    {
        public static WechatHybridUser GetWechatUser(this ClaimsPrincipal @this)
        {
            return new WechatHybridUser
            {
                OpenIdType = Enum.Parse<WechatHybridUser.EOpenIdType>(@this.GetClaim($"Wechat{nameof(WechatHybridUser.OpenIdType)}")),
                OpenId = @this.GetClaim($"Wechat{nameof(WechatHybridUser.OpenId)}"),
                PubUserName = @this.GetClaim($"Wechat{nameof(WechatHybridUser.PubUserName)}"),
                EntUserName = @this.GetClaim($"Wechat{nameof(WechatHybridUser.EntUserName)}"),
            };
        }

    }
}
