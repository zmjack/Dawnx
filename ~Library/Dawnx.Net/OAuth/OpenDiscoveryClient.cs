using Dawnx.Net.Web;
using Newtonsoft.Json.Linq;

namespace Dawnx.Net.OAuth
{
    public class OpenDiscoveryClient
    {
        public string Authority { get; }
        public string ConfigUrlPath { get; }
        public string OpenConfigUrl => $"{Authority}{ConfigUrlPath}";

        public OpenDiscoveryClient(string authority, string configUrlPath = "/.well-known/openid-configuration")
        {
            Authority = authority;
            ConfigUrlPath = configUrlPath;
        }

        public OpenDiscoveryResult Discovery()
        {
            var config = Web.Http.GetFor(OpenConfigUrl);
            return new OpenDiscoveryResult
            {
                Authority = Authority,
                TokenEndPointUrl = config["token_endpoint"].Value<string>(),
                UserInfoUrl = config["userinfo_endpoint"].Value<string>(),
            };
        }
    }
}
