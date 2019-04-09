using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace DawnxTemplate.Authorizations.WechatHybridAuthorize
{
    internal class WechatHybridRequirement : AuthorizationRequirementBase
    {
        public WechatHybridRequirement(string schema) : base(schema)
        {
        }
    }
}
