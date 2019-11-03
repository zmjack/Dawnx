using Dawnx.Security.AesSecurity;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Dawnx.Security
{
    public static class DawnAes
    {
        public static readonly byte[] EMPTY_IV = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static void FromBase64String(this Aes @this, string base64Key)
            => @this.Key = base64Key.BytesFromBase64();
        public static string ToBase64String(this Aes @this) => @this.Key.Base64String();

        public static void FromHexString(this Aes @this, string hexKey)
            => @this.Key = hexKey.BytesFromHex();
        public static string ToHexString(this Aes @this) => @this.Key.HexString();

        public static void SetEmptyIV(this Aes @this) => @this.IV = EMPTY_IV;

        /// <summary>
        /// </summary>
        /// <param name="this"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Encrypt<TAesCombiner>(this Aes @this, byte[] data)
            where TAesCombiner : IAesCombiner, new()
        {
            var combiner = new TAesCombiner().Then(x => x.Init(@this));

            var iv = @this.IV;
            var encryptor_memory = new MemoryStream();
            using (var encryptor = @this.CreateEncryptor())
            using (var crypto = new CryptoStream(encryptor_memory, encryptor, CryptoStreamMode.Write))
                crypto.Write(data, 0, data.Length);

            var ciphertext = encryptor_memory.ToArray();
            return combiner.Combine(ciphertext, iv);
        }
        public static byte[] Encrypt(this Aes @this, byte[] data)
        {
            if (@this.Mode == CipherMode.ECB)
                return Encrypt<AesEmptyIVCombiner>(@this, data);
            else return Encrypt<AesDefaultCombiner>(@this, data);
        }

        public static byte[] Decrypt<TAesCombiner>(this Aes @this, byte[] data)
            where TAesCombiner : IAesCombiner, new()
        {
            var combiner = new TAesCombiner();

            var separatedData = combiner.Separate(data);
            var iv = separatedData.iv;
            var ciphertext = separatedData.ciphertext;

            var decryptor_memory = new MemoryStream(ciphertext);
            using (var decryptor = @this.CreateDecryptor(@this.Key, iv))
            using (var crypto = new CryptoStream(decryptor_memory, decryptor, CryptoStreamMode.Read))
            {
                var data_memory = new MemoryStream();
                crypto.CopyTo(data_memory);
                return data_memory.ToArray();
            }
        }
        public static byte[] Decrypt(this Aes @this, byte[] data)
        {
            if (@this.Mode == CipherMode.ECB)
                return Decrypt<AesEmptyIVCombiner>(@this, data);
            else return Decrypt<AesDefaultCombiner>(@this, data);
        }

    }
}