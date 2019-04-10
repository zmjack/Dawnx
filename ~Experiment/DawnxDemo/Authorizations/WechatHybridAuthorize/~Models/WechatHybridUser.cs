using Dawnx;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace DawnxTemplate.Authorizations.WechatHybridAuthorize
{
    public class WechatHybridUser
    {
        public WechatHybridOpenIdType OpenIdType { get; set; }
        public string OpenId { get; set; }
        public string EntUserName { get; set; } = "";
        public string PubUserName { get; set; } = "";
        public string UserName
        {
            get
            {
                switch (OpenIdType)
                {
                    case WechatHybridOpenIdType.Public: return PubUserName;
                    case WechatHybridOpenIdType.Enterprise: return EntUserName;
                    default: throw new NotSupportedException();
                }
            }
        }

        public IIdentity ToIdentity()
        {
            return new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, UserName),
                new Claim($"Wechat{nameof(OpenIdType)}", OpenIdType.ToString()),
                new Claim($"Wechat{nameof(OpenId)}", OpenId),
                new Claim($"Wechat{nameof(EntUserName)}", EntUserName),
                new Claim($"Wechat{nameof(PubUserName)}", PubUserName),
            }, "WechatHybrid");
        }
    }
}
