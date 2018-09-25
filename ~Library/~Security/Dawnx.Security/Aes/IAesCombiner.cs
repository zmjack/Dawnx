using SystemAes = System.Security.Cryptography.Aes;

namespace Dawnx.Security.AesSecurity
{
    public interface IAesCombiner
    {
        void Init(SystemAes aes);
        byte[] Combine(byte[] ciphertext, byte[] iv);
        (byte[] ciphertext, byte[] iv) Separate(byte[] combinedData);
    }
}
