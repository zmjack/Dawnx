using Dawnx.Security.AesSecurity;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;

namespace Dawnx.Security
{
    public class TemporyTicket
    {
        public static TimeSpan SlideOffsetForExpiration = TimeSpan.FromMinutes(10);
        public static TemporyTicket Parse(AesProvider aesProvider, string ciphertext)
        {
            var json = aesProvider.Decrypt(ciphertext.BytesFromBase64()).String();
            return JsonConvert.DeserializeObject<TemporyTicket>(json);
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
            return aesProvider.Encrypt(json).Base64String();
        }

    }
}
