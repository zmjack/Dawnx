using Dawnx.Generators;
using Dawnx.Security.AesSecurity;
using NStandard;
using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace Dawnx.Security.Test
{
    public class AesTests
    {
        [Fact]
        public void Test1()
        {
            var key16 = Convert.ToBase64String(Encoding.Default.GetBytes(
                new StringGenerator("$w".Repeat(16)).Take(1)[0]));
            var key24 = Convert.ToBase64String(Encoding.Default.GetBytes(
                new StringGenerator("$w".Repeat(24)).Take(1)[0]));
            var key32 = Convert.ToBase64String(Encoding.Default.GetBytes(
                new StringGenerator("$w".Repeat(32)).Take(1)[0]));
            var key48 = Convert.ToBase64String(Encoding.Default.GetBytes(
                new StringGenerator("$w".Repeat(48)).Take(1)[0]));

            var text = Guid.NewGuid().ToString();
            var data = Encoding.UTF8.GetBytes(text);

            // 128 bits (16 bytes) key
            Aes.Create().Then(_ =>
            {
                _.FromBase64String(key16);
                Assert.Equal(_.Decrypt(_.Encrypt(data)), data);
            });

            // 192 bits (24 bytes) key
            Aes.Create().Then(_ =>
            {
                _.FromBase64String(key24);
                Assert.Equal(_.Decrypt(_.Encrypt(data)), data);
            });

            // 256 bits (32 bytes) key
            Aes.Create().Then(_ =>
            {
                _.FromBase64String(key32);
                Assert.Equal(_.Decrypt(_.Encrypt(data)), data);
            });

            // Not a key
            Aes.Create().Then(_ =>
            {
                Assert.Throws<CryptographicException>(() => _.FromBase64String(key48));
            });
        }

        [Fact]
        public void Test2()
        {
            Aes.Create().Then(_ =>
            {
                _.SetEmptyIV();
                _.Key = "1234567890123456".Bytes(Encoding.ASCII);

                Assert.Equal("0c66182ec710840065ebaa47c5e6ce90",
                    _.Encrypt<AesEmptyIVCombiner>("123".Bytes(Encoding.UTF8)).Flow(BytesFlows.HexString));
            });

            Aes.Create().Then(_ =>
            {
                var now = DateTime.Now.ToString();
                var encrypted = _.Encrypt(now.ToString().Bytes(Encoding.UTF8)).Flow(BytesFlows.Base64);
                Assert.Equal(now, _.Decrypt(encrypted.Flow(BytesFlows.FromBase64)).String(Encoding.UTF8));
            });
        }

        [Fact]
        public void Test3()
        {
            var aes1 = Aes.Create().Then(_ =>
            {
                _.SetEmptyIV();
                _.Key = "075ee2b75db7c4452c21fa9b33e89101".Bytes(Encoding.ASCII);
                _.Mode = CipherMode.CBC;
            });
            var aes2 = Aes.Create().Then(_ =>
            {
                _.SetEmptyIV();
                _.Key = "075ee2b75db7c4452c21fa9b33e89101".Bytes(Encoding.ASCII);
                _.Mode = CipherMode.CBC;
            });
            var ciphertext = aes1.Encrypt("123".Bytes());

            Assert.Equal("123", aes2.Decrypt(ciphertext).String());
        }

    }
}
