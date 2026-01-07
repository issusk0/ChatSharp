using System;
using System.Security.Cryptography;


namespace ChatSharp.Services
{
    public static class TokenGeneratorService
    {   
        public static string GenerateSecureToken(int length = 32)
        {
                // Generate random bytes
                byte[] randomBytes = new byte[length];
                RandomNumberGenerator.Fill(randomBytes); // Fills the array with cryptographically strong random values

                // Convert to a Base64 string (which is URL-safe if you replace + and /)
                string token = Convert.ToBase64String(randomBytes);
                
                // Make it URL-safe
                token = token.Replace("+", "-").Replace("/", "_").Replace("=", "");

                // Return the token (may be slightly longer than the requested byte length due to Base64 encoding)
                return token;
        }


    }

}