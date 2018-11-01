using Dawnx.Net.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Net.OAuth
{
    public class OpenDiscoveryResult
    {
        public string Authority { get; set; }
        public string TokenEndPointUrl { get; set; }
        public string UserInfoUrl { get; set; }
    }
}
