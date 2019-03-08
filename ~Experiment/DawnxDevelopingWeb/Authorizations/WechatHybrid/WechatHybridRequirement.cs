using Microsoft.AspNetCore.Authorization;

namespace DawnxDevelopingWeb.Authorizations.WechatHybrid
{
    internal class WechatHybridRequirement : IAuthorizationRequirement
    {
        public int Age { get; private set; }

        public WechatHybridRequirement(int age) { Age = age; }
    }
}