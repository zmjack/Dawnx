using Dawnx.AspNetCore;
using System;
using System.Security.Claims;

namespace DawnxTemplate.Authorizations.WechatHybridAuthorize
{
    public static class ClaimPricipalExtension
    {
        public static WechatHybridUser GetWechatHybridUser(this ClaimsPrincipal @this, string authenticationType = null)
        {
            return new WechatHybridUser
            {
                OpenIdType = Enum.Parse<WechatHybridOpenIdType>(@this.GetClaim(authenticationType, $"Wechat{nameof(WechatHybridUser.OpenIdType)}")),
                OpenId = @this.GetClaim(authenticationType, $"Wechat{nameof(WechatHybridUser.OpenId)}"),
                PubUserName = @this.GetClaim(authenticationType, $"Wechat{nameof(WechatHybridUser.PubUserName)}"),
                EntUserName = @this.GetClaim(authenticationType, $"Wechat{nameof(WechatHybridUser.EntUserName)}"),
            };
        }

    }
}
