using Dawnx.Security.AesSecurity;
using Xunit;

namespace Dawnx.Security.Test
{
    public class TemporyTicketTests
    {
        [Fact]
        public void Test1()
        {
            var aesProvider = new AesProvider()
                .WithKey(AesKey.Base64String, "cENaU1JhaERpaVF5dG5lQ05Fd2puQ1ZWSVVpWXZKaGE=");

            var ticket = new TemporyTicket
            {
                Data = "123",
            };
            var ciphertext = ticket.Ciphertext(aesProvider);

            var assumedTicket = TemporyTicket.Parse(aesProvider, ciphertext);
            Assert.Equal(ticket.Data, assumedTicket.Data);
            Assert.Equal(ticket.ExpirationTimeUtc, assumedTicket.ExpirationTimeUtc);
        }

    }
}
