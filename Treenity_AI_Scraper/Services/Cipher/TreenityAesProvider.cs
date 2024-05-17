using System.Security.Cryptography;
using System.Text;

namespace Treenity_AI_Scraper.Services.Cipher
{
    public class TreenityAesProvider
    {
        public byte[] Iv = Encoding.UTF8.GetBytes("1g3qqdh4jvbskb9x");
        public string Key = "";
        private Aes aes = Aes.Create();
        public TreenityAesProvider()
        {
            aes.IV = Iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
        }
        public string Encrypt(string jsonData,string key)
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            return Convert.ToBase64String(aes.EncryptCbc(Encoding.UTF8.GetBytes(jsonData),Iv));
        }
        public string Decrypt(string encryptedData, string key)
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            return Encoding.UTF8.GetString(aes.DecryptCbc(Convert.FromBase64String(encryptedData), Iv));
        }
    }
}
