using Dawnx.Security;
using NStandard;
using NStandard.Flows;
using System.Security.Cryptography;
using System.Text;

namespace Dawnx.AspNetCore
{
    public class SimpleAuthSecurity
    {
        private RSA _Rsa = RSA.Create();

        public void Init(string xmlString) => _Rsa.FromXmlStringStd(xmlString);

        public string Encrypt(string source)
            => _Rsa.Encrypt(source.Bytes()).Flow(BytesFlow.Base64);
        public string Decrypt(string encrypted)
            => _Rsa.Decrypt(encrypted.Flow(BytesFlow.FromBase64)).String(Encoding.UTF8);

        public string SignData(string data) =>
            _Rsa.SignData(data.Bytes(Encoding.UTF8)).Flow(BytesFlow.Base64);
        public bool VerifyData(string data, string signature) =>
            _Rsa.VerifyData(data.Bytes(Encoding.UTF8), signature.Flow(BytesFlow.FromBase64));

    }
}
