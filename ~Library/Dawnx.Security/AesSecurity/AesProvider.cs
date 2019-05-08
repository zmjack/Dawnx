using System;
using System.Security.Cryptography;

namespace Dawnx.Security.AesSecurity
{
    public class AesProvider : ISecurityProvider
    {
        public Aes WrappedAes { get; private set; }

        public AesProvider() : this(Aes.Create()) { }
        public AesProvider(Aes aes)
        {
            WrappedAes = aes;
            WrappedAes.Padding = PaddingMode.PKCS7;
        }

        public AesProvider WithKey(byte[] key)
        {
            WrappedAes.Key = key;
            return this;
        }

        public AesProvider WithKey(AesKey aesKey, string key)
        {
            ImportKey(aesKey, key);
            return this;
        }

        public void ImportKey(AesKey aesKey, string key)
        {
            switch (aesKey)
            {
                case AesKey.Base64String: WrappedAes.FromBase64String(key); return;
                case AesKey.HexString: WrappedAes.FromHexString(key); return;
            }
        }

        public string ExportKey(AesKey aesKey)
        {
            switch (aesKey)
            {
                case AesKey.Base64String: return WrappedAes.ToBase64String();
                case AesKey.HexString: return WrappedAes.ToHexString();
                default: throw new NotSupportedException();
            }
        }

        public virtual byte[] Encrypt(byte[] source) => WrappedAes.Encrypt(source);
        public virtual byte[] Decrypt(byte[] encrypted) => WrappedAes.Decrypt(encrypted);

    }
}
