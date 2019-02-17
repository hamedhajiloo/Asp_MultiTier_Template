using System;
using System.Linq;
using System.Security.Cryptography;

namespace Prj.Services.Utilities
{
    public class GeneratorService : IGeneratorService
    {
        public string GenerateClientId(int length)
        {
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[length];
            cryptoRandomDataGenerator.GetBytes(buffer);
            string unique = Convert.ToBase64String(buffer);
            return unique;
        }

        public string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
