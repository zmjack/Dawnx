using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Dawnx.Security.AesSecurity
{
    public class AesProvider<TAesCombiner> : AesProvider
        where TAesCombiner : IAesCombiner, new()
    {
        public Aes MappedAes { get; private set; }

        public AesProvider() : base() { }
        public AesProvider(Aes aes) : base(aes) { }

        public override byte[] Encrypt(byte[] source) => MappedAes.Encrypt<TAesCombiner>(source);
        public override byte[] Decrypt(byte[] encrypted) => MappedAes.Decrypt<TAesCombiner>(encrypted);

    }
}
