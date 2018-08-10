using System;
using System.Security.Cryptography;
using Xunit;
using System.Text;

namespace Dawnx.Security.Test
{
    public class RSATest1
    {
        [Fact]
        public void KeyImportTest()
        {
            // From Xml: Public
            RSA.Create().Self(_ =>
            {
                _.FromXmlStringStd(publicKey);
                AssertCheck_Public(_);
            });

            // From Xml: Public, Private
            RSA.Create().Self(_ =>
            {
                _.FromXmlStringStd(privateKey);
                AssertCheck_Public(_);
                AssertCheck_Private(_);
            });

            // From Pem: Public, Private
            RSA.Create().Self(_ =>
            {
                _.FromPemStringStd(privatePem);
                AssertCheck_Public(_);
            });

            // From Pem: Public, Private
            RSA.Create().Self(_ =>
            {
                _.FromPemStringStd(privatePem);
                AssertCheck_Public(_);
                AssertCheck_Private(_);
            });
        }

        [Fact]
        public void EncryptAndDecryptTest()
        {
            var text = Guid.NewGuid().ToString();
            var data = Encoding.UTF8.GetBytes(text);

            RSA.Create().Self(_ =>
            {
                Assert.Equal(_.Decrypt(_.Encrypt(data)), data);
            });
        }

        [Fact]
        public void SignAndVerifyTest()
        {
            var text = Guid.NewGuid().ToString();
            var data = Encoding.UTF8.GetBytes(text);

            RSA.Create().Self(_ =>
            {
                Assert.True(_.VerifyData(data, _.SignData(data)));
            });
        }

        private void AssertCheck_Public(RSA rsa)
        {
            Assert.Equal(publicKey, rsa.ToXmlStringStd(false));
            Assert.Equal(publicPem, rsa.ToPemStringStd(false));
        }

        private void AssertCheck_Private(RSA rsa)
        {
            Assert.Equal(privateKey, rsa.ToXmlStringStd(true));
            Assert.Equal(privatePem, rsa.ToPemStringStd(true));
        }

        string privateKey = @"<RSAKeyValue>
<Modulus>5uC3JbX86NqDi7H2XhR39GylpzbZqucDwre9tAOWeoiZim/ZvcVJTMcoiD+OkWgUgkYlt5pNvmi6fPYQFrZD0lHFMIGbzii12i15nfy7c8i2JV7eofOyFaByk6SVe4/3xVT9FJFymsxqMLIgTM4bE5t3Qww1gbKbrTYED17i2RC5dlxveoRDDnWMxr0nkVRvO6+wz1QR8E8uiqlq1LtzXblVEV4jCQjWOvF5bG7+2YYBslWJ6yewkTIRzZ/uJauzSiOjyU8frC8c8IK34maaTYE9152Q+A+qrMyarGBC+XODbVIM7Zaxpto2Bs0ACpi5x4vPJf/XR/8uQKN0JQ4MEQ==</Modulus>
<Exponent>AQAB</Exponent>
<P>7UE+eYkqLu/v5u+T+r904Ic0WjoOnCV1LEBw0qszMNbT2EMOdCyvQ1w30QNo+Q9qFACalzxsbfuOr+fgJ38fVkdjeqvjILRGEu/sLi+q9IpVOkTGjFT00FRRUHc2sorFRTZufxG4FEC6gJuGZPIs8z3ageeC92zwULv2VMt95y8=</P>
<Q>+R584Q27cGW/eqHFl4uK0ijDn0aDtUke78XxLgBEkxy1L5R3UxQ6qYOi4IU/a4FxHxJoyhErfy5fGpPYMAvVd3suPp93OFfVGJoX5zT0quW0tWwTBKLoA0iqIISo37PM1FEa+bTC2oOV0MjJs+C1l209xIqvlNBYPc6F2tQmcL8=</Q>
<DP>g+4C6pxei6k6syVIGWg7ettUPlQIacXeiVPwKQWwOplLRffL4sgyUXfHRf/qcIykxSiszip4dRQsfR6oo+3ppBWgeMd6TmZQjRlDMU+qdb8ys2spKUHYvLwWV3NjRBcsqVciTKCyxvhTfU5+hkWwvzYG+rOdPS8j1xEeYnqhsVs=</DP>
<DQ>Dtf7Num7jnHxm9wBywrchbM6HMZ12Jp3xm+z9Dq920otnZ0qEwA0kp8uWFR4N+6pj+Fn7wpg3h4kOpAupIY//PORCNg1oVzSbLnZzMQCBCDVyK2c4HzYeEGfKXreGR48iTYf9lsH9T878QnVwusTxucSdCCTX7meWGhy31wewj8=</DQ>
<InverseQ>aPkrzEi4tO3Nwd0Btu4jCmZV6egU2IRC786JJX5aqfWooKtXHQ4sMk7PovrEa0k9nA2BF/ZxhC3ad0tX12YyxzXwRUBZuPkl5VDFLglqWw1GJ7Yq4ifZiF2I3B2oxk0bwx+4S6l9s3XS25t5OxxATtV63pCnV9rc8CP8QK/Jhl8=</InverseQ>
<D>gGegVK/njhXhrXL3o5FcuasnYl1mJ9+9vrD0J8cLDPEl+9GDV0D/KF1nlIdMomAxb9bVqUx/SpPyjAdKIWeTRsjFk9mxrrIDmelKx2xFUPrjGtutlIL1m4OV1blmjccfNTe0XZkbFS71LEoVHsJOWtRPOxHyPET6whXVojrSjYfqLJ7AAGw9gEluxI2y+t+r0UUHBR1LFJvXi4wwZezIBZYBjVdgjGdsv4Vm/K57JkkhC4jlH+RR+AAIbmwP3dI0y/aCZTUatvMAglGRy8Q7x6pP482EdpfP9nMM7aZVJEqz2+F3TFzrQZURfLFc4yZHA1raP63Au5+gKe2i9VQ5pQ==</D>
</RSAKeyValue>";
        string publicKey = @"<RSAKeyValue>
<Modulus>5uC3JbX86NqDi7H2XhR39GylpzbZqucDwre9tAOWeoiZim/ZvcVJTMcoiD+OkWgUgkYlt5pNvmi6fPYQFrZD0lHFMIGbzii12i15nfy7c8i2JV7eofOyFaByk6SVe4/3xVT9FJFymsxqMLIgTM4bE5t3Qww1gbKbrTYED17i2RC5dlxveoRDDnWMxr0nkVRvO6+wz1QR8E8uiqlq1LtzXblVEV4jCQjWOvF5bG7+2YYBslWJ6yewkTIRzZ/uJauzSiOjyU8frC8c8IK34maaTYE9152Q+A+qrMyarGBC+XODbVIM7Zaxpto2Bs0ACpi5x4vPJf/XR/8uQKN0JQ4MEQ==</Modulus>
<Exponent>AQAB</Exponent>
</RSAKeyValue>";
        string privatePem = @"-----BEGIN PRIVATE KEY-----
MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDm4Lcltfzo2oOL
sfZeFHf0bKWnNtmq5wPCt720A5Z6iJmKb9m9xUlMxyiIP46RaBSCRiW3mk2+aLp8
9hAWtkPSUcUwgZvOKLXaLXmd/LtzyLYlXt6h87IVoHKTpJV7j/fFVP0UkXKazGow
siBMzhsTm3dDDDWBsputNgQPXuLZELl2XG96hEMOdYzGvSeRVG87r7DPVBHwTy6K
qWrUu3NduVURXiMJCNY68Xlsbv7ZhgGyVYnrJ7CRMhHNn+4lq7NKI6PJTx+sLxzw
grfiZppNgT3XnZD4D6qszJqsYEL5c4NtUgztlrGm2jYGzQAKmLnHi88l/9dH/y5A
o3QlDgwRAgMBAAECggEBAIBnoFSv544V4a1y96ORXLmrJ2JdZiffvb6w9CfHCwzx
JfvRg1dA/yhdZ5SHTKJgMW/W1alMf0qT8owHSiFnk0bIxZPZsa6yA5npSsdsRVD6
4xrbrZSC9ZuDldW5Zo3HHzU3tF2ZGxUu9SxKFR7CTlrUTzsR8jxE+sIV1aI60o2H
6iyewABsPYBJbsSNsvrfq9FFBwUdSxSb14uMMGXsyAWWAY1XYIxnbL+FZvyueyZJ
IQuI5R/kUfgACG5sD93SNMv2gmU1GrbzAIJRkcvEO8eqT+PNhHaXz/ZzDO2mVSRK
s9vhd0xc60GVEXyxXOMmRwNa2j+twLufoCntovVUOaUCgYEA7UE+eYkqLu/v5u+T
+r904Ic0WjoOnCV1LEBw0qszMNbT2EMOdCyvQ1w30QNo+Q9qFACalzxsbfuOr+fg
J38fVkdjeqvjILRGEu/sLi+q9IpVOkTGjFT00FRRUHc2sorFRTZufxG4FEC6gJuG
ZPIs8z3ageeC92zwULv2VMt95y8CgYEA+R584Q27cGW/eqHFl4uK0ijDn0aDtUke
78XxLgBEkxy1L5R3UxQ6qYOi4IU/a4FxHxJoyhErfy5fGpPYMAvVd3suPp93OFfV
GJoX5zT0quW0tWwTBKLoA0iqIISo37PM1FEa+bTC2oOV0MjJs+C1l209xIqvlNBY
Pc6F2tQmcL8CgYEAg+4C6pxei6k6syVIGWg7ettUPlQIacXeiVPwKQWwOplLRffL
4sgyUXfHRf/qcIykxSiszip4dRQsfR6oo+3ppBWgeMd6TmZQjRlDMU+qdb8ys2sp
KUHYvLwWV3NjRBcsqVciTKCyxvhTfU5+hkWwvzYG+rOdPS8j1xEeYnqhsVsCgYAO
1/s26buOcfGb3AHLCtyFszocxnXYmnfGb7P0Or3bSi2dnSoTADSSny5YVHg37qmP
4WfvCmDeHiQ6kC6khj/885EI2DWhXNJsudnMxAIEINXIrZzgfNh4QZ8pet4ZHjyJ
Nh/2Wwf1PzvxCdXC6xPG5xJ0IJNfuZ5YaHLfXB7CPwKBgGj5K8xIuLTtzcHdAbbu
IwpmVenoFNiEQu/OiSV+Wqn1qKCrVx0OLDJOz6L6xGtJPZwNgRf2cYQt2ndLV9dm
Msc18EVAWbj5JeVQxS4JalsNRie2KuIn2YhdiNwdqMZNG8MfuEupfbN10tubeTsc
QE7Vet6Qp1fa3PAj/ECvyYZf
-----END PRIVATE KEY-----";
        string publicPem = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA5uC3JbX86NqDi7H2XhR3
9GylpzbZqucDwre9tAOWeoiZim/ZvcVJTMcoiD+OkWgUgkYlt5pNvmi6fPYQFrZD
0lHFMIGbzii12i15nfy7c8i2JV7eofOyFaByk6SVe4/3xVT9FJFymsxqMLIgTM4b
E5t3Qww1gbKbrTYED17i2RC5dlxveoRDDnWMxr0nkVRvO6+wz1QR8E8uiqlq1Ltz
XblVEV4jCQjWOvF5bG7+2YYBslWJ6yewkTIRzZ/uJauzSiOjyU8frC8c8IK34maa
TYE9152Q+A+qrMyarGBC+XODbVIM7Zaxpto2Bs0ACpi5x4vPJf/XR/8uQKN0JQ4M
EQIDAQAB
-----END PUBLIC KEY-----";
    }
}
