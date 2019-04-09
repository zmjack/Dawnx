using System.Linq;
using System.Text.RegularExpressions;
using Dawnx.Analysises;
using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using DawnxTemplate.Authorizations.UserAuthorize;
using DawnxTemplate.Authorizations.WechatHybridAuthorize;
using System;

namespace DawnxTemplate.Authorizations
{
    public class CustomAuthorizationPolicyProvider : CustomExtraAuthorizationPolicyProvider
    {
        public CustomAuthorizationPolicyProvider() { }

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public override Type[] RequirementHandlerTypes => new[]
        {
            typeof(UserAuthorizationHandler),
            typeof(WechatHybridAuthorizationHandler),
        };

        public override IAuthorizationRequirement[] GetPolicyRequirements(string policyName)
        {
            var cargs = new ConsoleArgs(policyName);
            var schema = cargs["--schema"] ?? cargs["-s"];

            switch (cargs.Contents[0])
            {
                case nameof(WechatHybridAuthorize):
                    return new[] { new WechatHybridRequirement(schema) };

                case nameof(UserAuthorize):
                    return new[]
                    {
                        new UserAuthorizationRequirement(schema)
                        {
                            Users = cargs.Contents[1].Split("\t"),
                        },
                    };

                default: return null;
            }
        }

    }
}