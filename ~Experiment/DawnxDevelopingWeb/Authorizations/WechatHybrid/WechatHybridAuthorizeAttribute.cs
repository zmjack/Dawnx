using Microsoft.AspNetCore.Authorization;

namespace DawnxDevelopingWeb.Authorizations.WechatHybrid
{
    public class WechatHybridAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "WechatHybrid";

        public WechatHybridAuthorizeAttribute(int age) => Age = age;

        public int Age
        {
            get => int.TryParse(Policy.Substring(POLICY_PREFIX.Length + 1), out var age) ? age : default(int);
            set => Policy = $"{POLICY_PREFIX} {value.ToString()}";
        }
    }
}