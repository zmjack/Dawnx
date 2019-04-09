using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dawnx.AspNetCore.Authorization
{
    public abstract class CustomExtraAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public CustomExtraAuthorizationPolicyProvider() { }
        public CustomExtraAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var requirements = GetPolicyRequirements(policyName);

            if (requirements?.Any() ?? false)
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(requirements);
                return Task.FromResult(policy.Build());
            }

            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }

        public abstract IAuthorizationRequirement[] GetPolicyRequirements(string policyName);

        public abstract Type[] RequirementHandlerTypes { get; }

    }
}
