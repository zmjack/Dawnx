using Dawnx.Net.Http;
using Dawnx.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace Dawnx.Net.Test
{
    public class JwtTest
    {
        [Fact]
        public void Test1()
        {
            // HEADER: ALGORITHM & TOKEN TYPE
            var jwt_header = JsonConvert.SerializeObject(new
            {
                alg = "RS256",
                kid = "857f0787d67f7f36df7101e9449840a1",
                typ = "JWT",
            });
            Assert.Equal(
                "eyJhbGciOiJSUzI1NiIsImtpZCI6Ijg1N2YwNzg3ZDY3ZjdmMzZkZjcxMDFlOTQ0" +
                "OTg0MGExIiwidHlwIjoiSldUIn0",
                Base64Utility.ConvertBase64ToUrlBase64(jwt_header.Base64Encode()));

            // PAYLOAD: DATA
            var jwt_payload = JsonConvert.SerializeObject(new
            {
                nbf = 1539655112,
                exp = 1539658712,
                iss = "http://localhost.dawnx.net:5000",
                aud = new[]
                {
                    "http://localhost.dawnx.net:5000/resources",
                    "api1",
                },
                client_id = "client",
                scope = new[] { "api1" }
            });
            Assert.Equal(
                "eyJuYmYiOjE1Mzk2NTUxMTIsImV4cCI6MTUzOTY1ODcxMiwiaXNzIjoiaHR0cDov" +
                "L2xvY2FsaG9zdC5kYXdueC5uZXQ6NTAwMCIsImF1ZCI6WyJodHRwOi8vbG9jYWxo" +
                "b3N0LmRhd254Lm5ldDo1MDAwL3Jlc291cmNlcyIsImFwaTEiXSwiY2xpZW50X2lk" +
                "IjoiY2xpZW50Iiwic2NvcGUiOlsiYXBpMSJdfQ",
                Base64Utility.ConvertBase64ToUrlBase64(jwt_payload.Base64Encode()));

            // VERIFY SIGNATURE
            Assert.Equal(
                "ot1PHUJS-32DPnCzT_CSZUhpLaRaxU4Dy8aonpb9fJyxFVXuRFp4e7-oCgPq9EA_" +
                "Q87UnPQ7gC0BGWFUDEzvC8fY6wIoJcSqe57qY4W12BeOBtpBcxJ0U_H6Sc4P8zCb" +
                "ZkRMD6DvbYK6dLSDcFmh8Gj44f6abjZMqKqVGm-mE-y-zr10SfYdPCK-7z5W4t0M" +
                "Al9H7k-ptchnVAHztg9L0seq2TX88TsIbdnGSrknM_KDSaJugr2lfINMffGpSxuK" +
                "poL-sZW4XbczXHduCn11_n_DuJ8SJIeIhpa6kSO8dq7ElD5vTLesyD3_UJEZ2cuL" +
                "lztl7XTjm5R8FaHJALgWLw", "");
        }

        public void Test2()
        {
            var web = new WebAccess();
            var config = web.GetFor("http://localhost.dawnx.net:5000/.well-known/openid-configuration");
            var jwks = web.GetFor<JObject>(config["jwks_uri"].ToString())["keys"][0];

            var e = jwks["e"].ToString();
            var n = jwks["n"].ToString();

            var alg = jwks["alg"].ToString();
            var kid = jwks["kid"].ToString();

            var rsa = RSA.Create();
            rsa.ImportParameters(new RSAParameters
            {
                Exponent = Base64Utility.ConvertUrlBase64ToBase64(e).BytesFromBase64(),
                Modulus = Base64Utility.ConvertUrlBase64ToBase64(n).BytesFromBase64(),
            });

        }

    }
}
