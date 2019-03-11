using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dawnx.AspNetCore.Authorization;
using DawnxDevelopingWeb.Authorizations.MinimumAge;
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
            var regex = new Regex(@"([^\s]+) (.+)");
            var match = regex.Match(policyName);
            if (match.Success)
            {
                var prefix = match.Groups[1].Value;
                var value = match.Groups[2].Value;

                switch (prefix)
                {
                    case "MinimumAge":
                        return new[] { new MinimumAgeRequirement(int.Parse(value)) };

                    default: return null;
                }
            }
            else return null;
        }
    }
}