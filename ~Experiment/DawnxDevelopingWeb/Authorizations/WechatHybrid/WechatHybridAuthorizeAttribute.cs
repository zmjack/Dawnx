using Microsoft.AspNetCore.Authorization;

namespace DawnxDevelopingWeb.Authorizations.WechatHybrid
{
    public class WechatHybridAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "WechatHybrid";

        public WechatHybridAuthorizeAttribute()
        {
            Policy = "WechatHybrid";
        }
        
    }
}