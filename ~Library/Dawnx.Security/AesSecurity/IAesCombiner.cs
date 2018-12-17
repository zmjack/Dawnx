using System.Security.Cryptography;

namespace Dawnx.Security.AesSecurity
{
    public interface IAesCombiner
    {
        void Init(Aes aes);
        byte[] Combine(byte[] ciphertext, byte[] iv);
        (byte[] ciphertext, byte[] iv) Separate(byte[] combinedData);
    }
}
