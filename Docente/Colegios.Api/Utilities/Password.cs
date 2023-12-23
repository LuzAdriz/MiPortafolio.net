using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Colegios.Api.Utilities
{
    public class Password
    {
        public static string Generate(string password, int iterations = 1000)
        {
            var salt = new byte[24];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(24);

            var complete = Convert.ToBase64String(salt) + "|" + iterations.ToString() + "|" + Convert.ToBase64String(hash);

            return complete;
        }
        public static bool IsValid(string testPassword, string origDelimHash)
        {
            var origHashedParts = origDelimHash.Split('|');
            var origSalt = Convert.FromBase64String(origHashedParts[0]);
            var origIterations = int.Parse(origHashedParts[1]);
            var origHash = origHashedParts[2];

            var pbkdf2 = new Rfc2898DeriveBytes(testPassword, origSalt, origIterations);
            byte[] testHash = pbkdf2.GetBytes(24);

            return Convert.ToBase64String(testHash) == origHash;
        }
    }
}
