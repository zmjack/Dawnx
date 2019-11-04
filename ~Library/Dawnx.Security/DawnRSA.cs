using NStandard;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;

namespace Dawnx.Security
{
    public static class DawnRSA
    {
        public static void FromXmlStringStd(this RSA @this, string xmlString)
        {
            var @params = new RSAParameters();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Modulus": @params.Modulus = Convert.FromBase64String(node.InnerText); break;
                    case "Exponent": @params.Exponent = Convert.FromBase64String(node.InnerText); break;
                    case "P": @params.P = Convert.FromBase64String(node.InnerText); break;
                    case "Q": @params.Q = Convert.FromBase64String(node.InnerText); break;
                    case "DP": @params.DP = Convert.FromBase64String(node.InnerText); break;
                    case "DQ": @params.DQ = Convert.FromBase64String(node.InnerText); break;
                    case "InverseQ": @params.InverseQ = Convert.FromBase64String(node.InnerText); break;
                    case "D": @params.D = Convert.FromBase64String(node.InnerText); break;
                }
            }
            @this.ImportParameters(@params);
        }
        public static void FromXmlFileStd(this RSA @this, string path)
        {
            using (var file = new FileStream(path, FileMode.Open))
            using (var reader = new StreamReader(file))
            {
                FromXmlStringStd(@this, reader.ReadToEnd());
            }
        }

        public static void FromPemStringStd(this RSA @this, string pemString)
        {
            pemString = pemString.Replace("\r\n", string.Empty);

            string keyName;
            if (pemString.StartsWith("-----BEGIN PRIVATE KEY-----"))
                keyName = "PRIVATE";
            else if (pemString.StartsWith("-----BEGIN PUBLIC KEY-----"))
                keyName = "PUBLIC";
            else throw new ArgumentException("Can't analyze Pem string.");

            pemString = pemString
                .Replace($"-----BEGIN {keyName} KEY-----", string.Empty)
                .Replace($"-----END {keyName} KEY-----", string.Empty);
            var pem = Convert.FromBase64String(pemString);
            var @params = RsaConverter.ParamsFromPem(pem, keyName == "PRIVATE");

            @this.ImportParameters(@params);
        }
        public static void FromPemFileStd(this RSA @this, string path)
        {
            using (var file = new FileStream(path, FileMode.Open))
            using (var reader = new StreamReader(file))
                FromPemStringStd(@this, reader.ReadToEnd());
        }

        public static string ToXmlStringStd(this RSA @this, bool includePrivateParameters)
        {
            var @params = @this.ExportParameters(includePrivateParameters);

            if (includePrivateParameters)
            {
                return $"<RSAKeyValue>\r\n" +
                    $"<Modulus>{Convert.ToBase64String(@params.Modulus)}</Modulus>\r\n" +
                    $"<Exponent>{Convert.ToBase64String(@params.Exponent)}</Exponent>\r\n" +
                    $"<P>{Convert.ToBase64String(@params.P)}</P>\r\n" +
                    $"<Q>{Convert.ToBase64String(@params.Q)}</Q>\r\n" +
                    $"<DP>{Convert.ToBase64String(@params.DP)}</DP>\r\n" +
                    $"<DQ>{Convert.ToBase64String(@params.DQ)}</DQ>\r\n" +
                    $"<InverseQ>{Convert.ToBase64String(@params.InverseQ)}</InverseQ>\r\n" +
                    $"<D>{Convert.ToBase64String(@params.D)}</D>\r\n" +
                    $"</RSAKeyValue>";
            }
            else
            {
                return $"<RSAKeyValue>\r\n" +
                    $"<Modulus>{Convert.ToBase64String(@params.Modulus)}</Modulus>\r\n" +
                    $"<Exponent>{Convert.ToBase64String(@params.Exponent)}</Exponent>\r\n" +
                    $"</RSAKeyValue>";
            }
        }

        public static string ToPemStringStd(this RSA @this, bool includePrivateParameters)
        {
            var keyName = includePrivateParameters ? "PRIVATE" : "PUBLIC";
            var @params = @this.ExportParameters(includePrivateParameters);
            var base64 = string.Join(Environment.NewLine, Convert.ToBase64String(
                RsaConverter.ParamsToPem(@params, includePrivateParameters))
                .ToCharArray().AsKvPairs()
                .GroupBy(x => x.Key / 64)
                .Select(g => new string(g.Select(x => x.Value).ToArray())));

            return $"-----BEGIN {keyName} KEY-----\r\n{base64}\r\n-----END {keyName} KEY-----";
        }

        public static byte[] Encrypt(this RSA @this, byte[] data)
            => @this.Encrypt(data, RSAEncryptionPadding.Pkcs1);

        public static byte[] Decrypt(this RSA @this, byte[] data)
            => @this.Decrypt(data, RSAEncryptionPadding.Pkcs1);

        public static byte[] SignData(this RSA @this, byte[] data)
            => @this.SignData(data, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

        public static bool VerifyData(this RSA @this, byte[] data, byte[] signature)
            => @this.VerifyData(data, signature, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
    }
}