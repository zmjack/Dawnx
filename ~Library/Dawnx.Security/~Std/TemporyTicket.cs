using Dawnx.Security.AesSecurity;
using Newtonsoft.Json;
using NStandard;
using System;

namespace Dawnx.Security
{
    public class TemporaryTicket
    {
        public static TimeSpan SlideOffsetForExpiration = TimeSpan.FromMinutes(10);
        public static TemporaryTicket Parse(AesProvider aesProvider, string ciphertext)
        {
            var json = aesProvider.Decrypt(ciphertext.Flow(BytesFlows.FromUrlSafeBase64)).String();
            return JsonConvert.DeserializeObject<TemporaryTicket>(json);
        }

        /// <summary>
        /// Checks whether the ticket is valid. (The default <see cref="SlideOffsetForExpiration"/> is 10 minutes.)
        /// </summary>
        public bool IsValid => DateTime.UtcNow.For(
            _ => ExpirationTimeUtc - SlideOffsetForExpiration <= _
                && _ <= ExpirationTimeUtc + SlideOffsetForExpiration);

        public string Data { get; set; }

        public DateTime _ExpirationTimeUtc = DateTime.UtcNow.AddMinutes(1);
        public DateTime ExpirationTimeUtc
        {
            get => _ExpirationTimeUtc;
            set => _ExpirationTimeUtc = value.ToUniversalTime();
        }

        public string Ciphertext(AesProvider aesProvider)
        {
            var json = JsonConvert.SerializeObject(this).Bytes();
            return aesProvider.Encrypt(json).Flow(BytesFlows.UrlSafeBase64);
        }

    }
}
