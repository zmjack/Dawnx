using System.Security.Cryptography;

namespace Dawnx.Security.AesSecurity
{
    public class AesEmptyIVCombiner : IAesCombiner
    {
        public void Init(Aes aes) => aes.SetEmptyIV();
        public byte[] Combine(byte[] ciphertext, byte[] iv) => ciphertext;
        public (byte[] ciphertext, byte[] iv) Separate(byte[] combinedData) => (combinedData, DawnAes.EMPTY_IV);
    }
}
