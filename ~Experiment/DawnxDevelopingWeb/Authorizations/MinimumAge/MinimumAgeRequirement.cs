using Microsoft.AspNetCore.Authorization;

namespace DawnxDevelopingWeb.Authorizations.MinimumAge
{
    internal class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int Age { get; private set; }

        public MinimumAgeRequirement(int age) { Age = age; }
    }
}