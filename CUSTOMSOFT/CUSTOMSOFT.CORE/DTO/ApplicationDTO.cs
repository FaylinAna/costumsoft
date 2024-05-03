
using System;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CUSTOMSOFT.CORE.DTO
{
    public class ApplicationDTO
    {
        private string name;
        private string apiKey;
        private string secretKey;
        private string privateKey;
        private string clientSecretKey;
        public string Name => name; 
        public string ApiKey => apiKey;
        public string SecretKey => secretKey;
        public string PrivateKey => privateKey;
        public string ClientSecretKey => clientSecretKey;

        private ApplicationDTO(string name, string apiKey, string clientSecretKey, string privateKey)
        {
            this.name = name;
            this.apiKey = apiKey;
            this.secretKey = clientSecretKey;
            this.privateKey = privateKey;
            this.clientSecretKey = clientSecretKey;
        }

        public ApplicationDTO(string name)
        {
            this.name = name;
            apiKey = Guid.NewGuid().ToString("N");
            secretKey = Guid.NewGuid().ToString("N");
            privateKey = Guid.NewGuid().ToString("N");
            clientSecretKey = Cipher(secretKey, privateKey);
        }

        
        public static ApplicationDTO FromEntity(string name, string apiKey, string clientSecretKey, string privateKey)
        {
            return new ApplicationDTO(name, apiKey, clientSecretKey, privateKey);
        }

        public static bool Authenticate(string appName, string apiKey,string privateKey,string secretKey, string clientSecretKey)
        {
           
            bool authenticated = false;

                var encryptedSecretKey = Cipher(clientSecretKey, privateKey);
                if (encryptedSecretKey == secretKey)
                {
                    authenticated = true;
                }
            return authenticated;



        }
        public static string Cipher(string plainText, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var shaAlgorithm = SHA256.Create())
            {
                var combinedHash = new byte[passwordBytes.Length + saltBytes.Length];
                Buffer.BlockCopy(passwordBytes, 0, combinedHash, 0, passwordBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, combinedHash, passwordBytes.Length, saltBytes.Length);
                return Convert.ToBase64String(shaAlgorithm.ComputeHash(combinedHash));
            }
        }
    }
}
