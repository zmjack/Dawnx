using Microsoft.AspNetCore.Authorization;

namespace DawnxDevelopingWeb.Authorizations.MinimumAge
{
    public class MinimumAgeAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "MinimumAge";

        public MinimumAgeAuthorizeAttribute(int age) => Age = age;

        public int Age
        {
            get => int.TryParse(Policy.Substring(POLICY_PREFIX.Length + 1), out var age) ? age : default(int);
            set => Policy = $"{POLICY_PREFIX} {value.ToString()}";
        }
    }
}