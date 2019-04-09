using Dawnx.Security.RsaSecurity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DawnxDevelopingWeb
{
    public class Program
    {
        public RsaProvider RemoteAuthSecurity = new RsaProvider().WithKey(RsaKey.Xml, @"
<RSAKeyValue>
<Modulus>1MFVJkRE9ck16WqP/b/bnZnMDYObJzsufskvdrq1Vyi5tEilum2Tk4EplxtELsu3yQ8nVKYLvipDtp47o+gKE4qn207Cr2xNa8AwGn6C+H13ynG6qimdEFzXVYP8YKWI15AHXzisWjf4Fogql+6S+DxsQNzmSF1JlG+hcldvARv+GX9XHtfzFbO5DOmoR1g1BqaAFUCqVBKRvOcsLc+KA0nL0GgZD68cBvZFpg0BU4DSvnHcObgV4J7Rgw0vGZhlVcRCLV1dmdhIPQl4kw/v6hNwHsnONADUmFlfPMZHOQepentYF9WRg9GRzUVOMem6mI4NgD6ys9RIEbmlsJEm9Q==</Modulus>
<Exponent>AQAB</Exponent>
</RSAKeyValue>");

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }
}
