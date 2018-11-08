using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Dawnx.Security.RsaSecurity
{
    public class RsaProvider : ISecurityProvider
    {
        public RSA WrappedRsa { get; private set; }

        public RsaProvider() : this(RSA.Create()) { }
        public RsaProvider(RSA rsa)
        {
            WrappedRsa = rsa;
        }

        public RsaProvider WithKey(RsaKey rsaKey, string key)
        {
            ImportKey(rsaKey, key);
            return this;
        }

        public void ImportKey(RsaKey rsaKey, string key)
        {
            switch (rsaKey)
            {
                case RsaKey.Pem: WrappedRsa.FromPemStringStd(key); return;
                case RsaKey.Xml: WrappedRsa.FromXmlStringStd(key); return;
            }
        }

        public string ExportKey(RsaKey aesKey, bool includePrivateParameters)
        {
            switch (aesKey)
            {
                case RsaKey.Pem: return WrappedRsa.ToPemStringStd(includePrivateParameters);
                case RsaKey.Xml: return WrappedRsa.ToXmlStringStd(includePrivateParameters);
                default: throw new NotSupportedException();
            }
        }

        public byte[] Encrypt(byte[] source) => WrappedRsa.Encrypt(source);
        public byte[] Decrypt(byte[] encrypted) => WrappedRsa.Decrypt(encrypted);

        public byte[] Sign(byte[] data) => WrappedRsa.SignData(data);
        public bool Verify(byte[] data, byte[] signature) => WrappedRsa.VerifyData(data, signature);

    }
}
