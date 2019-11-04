using NStandard;
using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace Dawnx.Security.Test
{
    public class RsaTests
    {
        [Fact]
        public void ImportPublicPemTest()
        {
            RSA.Create().Then(_ =>
            {
                _.FromPemStringStd(publicPem);
                AssertCheck_Public(_);
            });
        }

        [Fact]
        public void ImportPemTest()
        {
            RSA.Create().Then(_ =>
            {
                _.FromPemStringStd(privatePem);
                AssertCheck_Public(_);
                AssertCheck_Private(_);
            });
        }

        [Fact]
        public void ImportPublicXmlTest()
        {
            RSA.Create().Then(_ =>
            {
                _.FromXmlStringStd(publicKey);
                AssertCheck_Public(_);
            });
        }

        [Fact]
        public void ImportXmlTest()
        {
            RSA.Create().Then(_ =>
            {
                _.FromXmlStringStd(privateKey);
                AssertCheck_Public(_);
                AssertCheck_Private(_);
            });
        }

        [Fact]
        public void EncryptAndDecryptTest()
        {
            var text = Guid.NewGuid().ToString();
            var data = Encoding.UTF8.GetBytes(text);

            RSA.Create().Then(_ =>
            {
                Assert.Equal(_.Decrypt(_.Encrypt(data)), data);
            });
        }

        [Fact]
        public void SignAndVerifyTest()
        {
            var text = Guid.NewGuid().ToString();
            var data = Encoding.UTF8.GetBytes(text);

            RSA.Create().Then(_ =>
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
            // "D" is different between a creator and a non-creator while outputting parameters
            Assert.True(rsa.ToXmlStringStd(true).For(_ => _ == privateKey || _ == privateKey_NonCreator));
            Assert.True(rsa.ToPemStringStd(true).For(_ => _ == privatePem || _ == privatePem_NonCreator));
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

        string privateKey_NonCreator = @"<RSAKeyValue>
<Modulus>5uC3JbX86NqDi7H2XhR39GylpzbZqucDwre9tAOWeoiZim/ZvcVJTMcoiD+OkWgUgkYlt5pNvmi6fPYQFrZD0lHFMIGbzii12i15nfy7c8i2JV7eofOyFaByk6SVe4/3xVT9FJFymsxqMLIgTM4bE5t3Qww1gbKbrTYED17i2RC5dlxveoRDDnWMxr0nkVRvO6+wz1QR8E8uiqlq1LtzXblVEV4jCQjWOvF5bG7+2YYBslWJ6yewkTIRzZ/uJauzSiOjyU8frC8c8IK34maaTYE9152Q+A+qrMyarGBC+XODbVIM7Zaxpto2Bs0ACpi5x4vPJf/XR/8uQKN0JQ4MEQ==</Modulus>
<Exponent>AQAB</Exponent>
<P>7UE+eYkqLu/v5u+T+r904Ic0WjoOnCV1LEBw0qszMNbT2EMOdCyvQ1w30QNo+Q9qFACalzxsbfuOr+fgJ38fVkdjeqvjILRGEu/sLi+q9IpVOkTGjFT00FRRUHc2sorFRTZufxG4FEC6gJuGZPIs8z3ageeC92zwULv2VMt95y8=</P>
<Q>+R584Q27cGW/eqHFl4uK0ijDn0aDtUke78XxLgBEkxy1L5R3UxQ6qYOi4IU/a4FxHxJoyhErfy5fGpPYMAvVd3suPp93OFfVGJoX5zT0quW0tWwTBKLoA0iqIISo37PM1FEa+bTC2oOV0MjJs+C1l209xIqvlNBYPc6F2tQmcL8=</Q>
<DP>g+4C6pxei6k6syVIGWg7ettUPlQIacXeiVPwKQWwOplLRffL4sgyUXfHRf/qcIykxSiszip4dRQsfR6oo+3ppBWgeMd6TmZQjRlDMU+qdb8ys2spKUHYvLwWV3NjRBcsqVciTKCyxvhTfU5+hkWwvzYG+rOdPS8j1xEeYnqhsVs=</DP>
<DQ>Dtf7Num7jnHxm9wBywrchbM6HMZ12Jp3xm+z9Dq920otnZ0qEwA0kp8uWFR4N+6pj+Fn7wpg3h4kOpAupIY//PORCNg1oVzSbLnZzMQCBCDVyK2c4HzYeEGfKXreGR48iTYf9lsH9T878QnVwusTxucSdCCTX7meWGhy31wewj8=</DQ>
<InverseQ>aPkrzEi4tO3Nwd0Btu4jCmZV6egU2IRC786JJX5aqfWooKtXHQ4sMk7PovrEa0k9nA2BF/ZxhC3ad0tX12YyxzXwRUBZuPkl5VDFLglqWw1GJ7Yq4ifZiF2I3B2oxk0bwx+4S6l9s3XS25t5OxxATtV63pCnV9rc8CP8QK/Jhl8=</InverseQ>
<D>DPdEwdTpGaif55n8dIcgv3TUjsH5Umw73VUVTcU/z6zZNpmWeF5agfnTUGeFWawnLrPCzX9Ya1+VTYxCFgxxXZ/i+5jjx52orNKN+G3nlxaICCw+Q4kckLNci+cbz/8jUo0101Bhx8jAE/EE+FtA0QaTmYvXe2us63rTmothIQCAoU41jpzro+ZZKdvoV7VNi2krXbxq075OSWh7UUrwUH3a8GsyqFf4EfoDCssuAfO5u9/Q0RxwRF3kxXhEkHbCCC2NJjq3ZukHzxJAjOC+WO6o0GuEdn5kbop4FWX8xtn+6P0tOM8J0FAfK3LpR0svdSEV5ccbNkRQTtoAsp9fkw==</D>
</RSAKeyValue>";
        string privatePem_NonCreator = @"-----BEGIN PRIVATE KEY-----
MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDm4Lcltfzo2oOL
sfZeFHf0bKWnNtmq5wPCt720A5Z6iJmKb9m9xUlMxyiIP46RaBSCRiW3mk2+aLp8
9hAWtkPSUcUwgZvOKLXaLXmd/LtzyLYlXt6h87IVoHKTpJV7j/fFVP0UkXKazGow
siBMzhsTm3dDDDWBsputNgQPXuLZELl2XG96hEMOdYzGvSeRVG87r7DPVBHwTy6K
qWrUu3NduVURXiMJCNY68Xlsbv7ZhgGyVYnrJ7CRMhHNn+4lq7NKI6PJTx+sLxzw
grfiZppNgT3XnZD4D6qszJqsYEL5c4NtUgztlrGm2jYGzQAKmLnHi88l/9dH/y5A
o3QlDgwRAgMBAAECggEADPdEwdTpGaif55n8dIcgv3TUjsH5Umw73VUVTcU/z6zZ
NpmWeF5agfnTUGeFWawnLrPCzX9Ya1+VTYxCFgxxXZ/i+5jjx52orNKN+G3nlxaI
CCw+Q4kckLNci+cbz/8jUo0101Bhx8jAE/EE+FtA0QaTmYvXe2us63rTmothIQCA
oU41jpzro+ZZKdvoV7VNi2krXbxq075OSWh7UUrwUH3a8GsyqFf4EfoDCssuAfO5
u9/Q0RxwRF3kxXhEkHbCCC2NJjq3ZukHzxJAjOC+WO6o0GuEdn5kbop4FWX8xtn+
6P0tOM8J0FAfK3LpR0svdSEV5ccbNkRQTtoAsp9fkwKBgQDtQT55iSou7+/m75P6
v3TghzRaOg6cJXUsQHDSqzMw1tPYQw50LK9DXDfRA2j5D2oUAJqXPGxt+46v5+An
fx9WR2N6q+MgtEYS7+wuL6r0ilU6RMaMVPTQVFFQdzayisVFNm5/EbgUQLqAm4Zk
8izzPdqB54L3bPBQu/ZUy33nLwKBgQD5HnzhDbtwZb96ocWXi4rSKMOfRoO1SR7v
xfEuAESTHLUvlHdTFDqpg6LghT9rgXEfEmjKESt/Ll8ak9gwC9V3ey4+n3c4V9UY
mhfnNPSq5bS1bBMEougDSKoghKjfs8zUURr5tMLag5XQyMmz4LWXbT3Eiq+U0Fg9
zoXa1CZwvwKBgQCD7gLqnF6LqTqzJUgZaDt621Q+VAhpxd6JU/ApBbA6mUtF98vi
yDJRd8dF/+pwjKTFKKzOKnh1FCx9Hqij7emkFaB4x3pOZlCNGUMxT6p1vzKzaykp
Qdi8vBZXc2NEFyypVyJMoLLG+FN9Tn6GRbC/Ngb6s509LyPXER5ieqGxWwKBgA7X
+zbpu45x8ZvcAcsK3IWzOhzGddiad8Zvs/Q6vdtKLZ2dKhMANJKfLlhUeDfuqY/h
Z+8KYN4eJDqQLqSGP/zzkQjYNaFc0my52czEAgQg1citnOB82HhBnyl63hkePIk2
H/ZbB/U/O/EJ1cLrE8bnEnQgk1+5nlhoct9cHsI/AoGAaPkrzEi4tO3Nwd0Btu4j
CmZV6egU2IRC786JJX5aqfWooKtXHQ4sMk7PovrEa0k9nA2BF/ZxhC3ad0tX12Yy
xzXwRUBZuPkl5VDFLglqWw1GJ7Yq4ifZiF2I3B2oxk0bwx+4S6l9s3XS25t5OxxA
TtV63pCnV9rc8CP8QK/Jhl8=
-----END PRIVATE KEY-----";
    }
}
