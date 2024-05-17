using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace Treenity_AI_Scraper.Services.Cipher
{
    public class TreenityRsaProvider
    {
        private RSA rsa = RSA.Create();
        public byte[] PublicKey = Convert.FromBase64String("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCgfZmpLpPEpEFRKBe+ZjWJUjPe+7qg7pGqcfN3j2egJ8H2mrKwaEqZEnPnpi2O3hN8HRyaFozDOp8gwZiYfiIZjWy0Jr/FNAiiKYh5bq0GsEn+ieMmRyJg/+i1rqizhvCXvFdrdGhFTw5EBwTpsGdwe1utdlrvIJUAFWj9Yh4qbQIDAQAB");
        public TreenityRsaProvider()
        {
            rsa.ImportSubjectPublicKeyInfo(PublicKey, out _);
        }
        public string Encrypt(string jsonData)
        {
            return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(jsonData),RSAEncryptionPadding.Pkcs1));
        }
        public string Decrypt(string data)
        {
            AsymmetricKeyParameter keyPair = DotNetUtilities.GetRsaPublicKey(rsa);
            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");  
            c.Init(false, keyPair);
            byte[] DataToEncrypt = Convert.FromBase64String(data);
            byte[] outBytes = c.DoFinal(DataToEncrypt);
            return Encoding.UTF8.GetString(outBytes);
        }
    }
}
