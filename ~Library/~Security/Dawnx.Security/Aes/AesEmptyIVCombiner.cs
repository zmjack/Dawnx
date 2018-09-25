using SystemAes = System.Security.Cryptography.Aes;

namespace Dawnx.Security.Aes
{
    public class AesEmptyIVCombiner : IAesCombiner
    {
        public void Init(SystemAes aes) => aes.SetEmptyIV();
        public byte[] Combine(byte[] ciphertext, byte[] iv) => ciphertext;
        public (byte[] ciphertext, byte[] iv) Separate(byte[] combinedData) => (combinedData, DawnAes.EMPTY_IV);
    }
}
