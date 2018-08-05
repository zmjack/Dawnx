using Xunit;

namespace Dawnx.AspNetCore.Test
{
    public class SimpleAuthSecurityTest
    {
        [Fact]
        public void Test1()
        {
            var security = new SimpleAuthSecurity();
            var time = "2018-6-4 11:22:33";

            var encrypted = security.Encrypt(time);
            var decrypted = security.Decrypt(encrypted);

            Assert.Equal(time, decrypted);
        }

    }
}
