using Dawnx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DawnxDevelopingWeb.Authorizations.WechatHybrid
{
    public class WechatHybridUser
    {
        public enum EOpenIdType { Public, Enterprise }

        public EOpenIdType OpenIdType { get; set; }
        public string OpenId { get; set; }
        public string EntUserName { get; set; }
        public string PubUserName { get; set; }

        public IIdentity ToIdentity()
        {
            return new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, OpenIdType.For(type =>
                {
                    switch(type)
                    {
                        case EOpenIdType.Public: return PubUserName;
                        case EOpenIdType.Enterprise: return EntUserName;
                        default: throw new NotSupportedException();
                    }
                })),
                new Claim($"Wechat{nameof(OpenIdType)}", OpenIdType.ToString()),
                new Claim($"Wechat{nameof(OpenId)}", OpenId),
                new Claim($"Wechat{nameof(EntUserName)}", EntUserName),
                new Claim($"Wechat{nameof(PubUserName)}", PubUserName),
            }, "DawnxWechat");
        }
    }
}
