using NStandard;
using System.Linq;
using System.Security.Cryptography;

namespace Dawnx.Security.AesSecurity
{
    public class AesDefaultCombiner : IAesCombiner
    {
        public void Init(Aes aes) => aes.GenerateIV();
        public byte[] Combine(byte[] ciphertext, byte[] iv) => iv.Concat(ciphertext).ToArray();

        public (byte[] ciphertext, byte[] iv) Separate(byte[] combinedData)
            => (combinedData.Slice(16), combinedData.Slice(0, 16));
    }
}
