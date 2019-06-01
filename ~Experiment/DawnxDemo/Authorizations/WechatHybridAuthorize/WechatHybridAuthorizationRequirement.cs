using Dawnx.AspNetCore.Authorization;

namespace DawnxTemplate.Authorizations.WechatHybridAuthorize
{
    internal class WechatHybridRequirement : AuthorizationRequirementBase
    {
        public WechatHybridRequirement(string authenticationType) : base(authenticationType)
        {
        }
    }
}
