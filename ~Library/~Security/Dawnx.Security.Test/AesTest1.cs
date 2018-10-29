using System;
using System.Security.Cryptography;
using Xunit;
using System.Text;
using Dawnx.Generators;
using Dawnx.Security.AesSecurity;

namespace Dawnx.Security.Test
{
    public class AesTest1
    {
        [Fact]
        public void Test1()
        {
            var key16 = Convert.ToBase64String(Encoding.Default.GetBytes(
                new StringGenerator("$w".Replicate(16)).Take(1)[0]));
            var key24 = Convert.ToBase64String(Encoding.Default.GetBytes(
                new StringGenerator("$w".Replicate(24)).Take(1)[0]));
            var key32 = Convert.ToBase64String(Encoding.Default.GetBytes(
                new StringGenerator("$w".Replicate(32)).Take(1)[0]));
            var key48 = Convert.ToBase64String(Encoding.Default.GetBytes(
                new StringGenerator("$w".Replicate(48)).Take(1)[0]));

            var text = Guid.NewGuid().ToString();
            var data = Encoding.UTF8.GetBytes(text);

            // 128 bits (16 bytes) key
            Aes.Create().Self(_ =>
            {
                _.FromBase64String(key16);
                Assert.Equal(_.Decrypt(_.Encrypt(data)), data);
            });

            // 192 bits (24 bytes) key
            Aes.Create().Self(_ =>
            {
                _.FromBase64String(key24);
                Assert.Equal(_.Decrypt(_.Encrypt(data)), data);
            });

            // 256 bits (32 bytes) key
            Aes.Create().Self(_ =>
            {
                _.FromBase64String(key32);
                Assert.Equal(_.Decrypt(_.Encrypt(data)), data);
            });

            // Not a key
            Aes.Create().Self(_ =>
            {
                Assert.Throws<CryptographicException>(() => _.FromBase64String(key48));
            });
        }

        [Fact]
        public void Test2()
        {
            Aes.Create().Self(_ =>
            {
                _.SetEmptyIV();
                _.Key = "1234567890123456".Bytes(Encoding.ASCII);

                Assert.Equal("0c66182ec710840065ebaa47c5e6ce90",
                    _.Encrypt<AesEmptyIVCombiner>("123".Bytes(Encoding.UTF8)).HexString());
            });

            Aes.Create().Self(_ =>
            {
                var now = DateTime.Now.ToString();
                var encrypted = _.Encrypt(now.ToString().Bytes(Encoding.UTF8)).Base64String();
                Assert.Equal(now, _.Decrypt(encrypted.BytesFromBase64()).String(Encoding.UTF8));
            });
        }

        [Fact]
        public void Test3()
        {
            Aes.Create().Self(_ =>
            {
                _.SetEmptyIV();
                _.Key = "075ee2b75db7c4452c21fa9b33e89101".Bytes(Encoding.ASCII);
                _.Mode = CipherMode.ECB;

                var bytes = "7DCC9959F27D9CC891DDDA48FE7B7AD8C0F56696E683A3495DF6EBB877A839C7D8E0AAF9FB03EB791B10C6C0582A562C4D1D24AABC45260088131E50760283B88826F7ED1BE502567CD3CCC149B83620F3AED52A5AF5D113453A69DF1D36F7C27721FE0B685B2AB94FA283537DAE23C6A5279FB704DBBAF40AD31D9FDF38BDFF7CCBF66AFA20DFBE5694421EEAE5A6DC8B963572057F97D770D9674DBA78466DCEAF3012B07E3C5AA4C6666E739B54258CCDF608D0A0A28F9D4E47113DD427806210B0790EEF985BEF7BF0EADE7584D7B94E23E3A81DD060FEF9DC8BA36F8A3200E7E27579DF766388CC378F72306263CB9C894CD84A782FF45F720C4ACB35C8E9F6FC603E2789F515EE6F35E842F96A4C3B7051D2F4C3A5BB7E76109B271CE9D3D83AB0C30A58F10EBEB4887C691C6248698C6957C07144D47F3F71D2CE9518EA054DA6C81FE202E607AAFD92904AAE15003B8BA24AB5E7E2066FDACC81AD7DD6EFE0737E39438D822B2E4373D6DC5C80102B6DABB79D29F0354A063F195BF7D579C62BC6FBA12F001A278A9950DA09095D78C7016AA35E4E88FE5B95E759C4185D424755F00E89A7FD4D3DF8CFF247B7CE8A1C21502F6C2078AD700A546611B4951B873CD23D10F61DE901D7BB791A76E48DDD2DDC7C7D95E6EE24629E24CF12FE44BFC1E4931FF7CC6479C80C24495A63335F548AF568F087E5312FBB94A3AEB27EB95EB0B54990297E4953106EAB"
                .BytesFromHex();

                var aa = _.Decrypt<AesDefaultCombiner>(bytes).String(Encoding.UTF8);
            });
        }

    }
}
