using System.Text.RegularExpressions;
using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace DawnxDevelopingWeb.Authorizations.WechatHybrid
{
    public class WechatHybridExtraPolicyProvider : CustomExtraPolicyProvider
    {
        public WechatHybridExtraPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public override IAuthorizationRequirement[] GetPolicyRequirements(string policyName)
        {
            var regex = new Regex(@"([^\s]+) ?(.*)");
            var match = regex.Match(policyName);
            if (match.Success)
            {
                var prefix = match.Groups[1].Value;
                var value = match.Groups[2].Value;

                switch (prefix)
                {
                    case "WechatHybrid":
                        return new[] { new WechatHybridRequirement() };

                    default: return null;
                }
            }
            else return null;
        }
    }
}