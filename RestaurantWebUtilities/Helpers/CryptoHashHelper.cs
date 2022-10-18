using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace RestaurantWebUtilities.Helpers;

public static class CryptoHashHelper
{
    /// <summary>
    /// Hashes a plain text password using Pbkdf2 algorithm
    /// </summary>
    /// <param name="plainTextPassword">Plain text password to be hashed</param>
    /// <param name="salt">128 bit long random salt</param>
    /// <returns>256 bit long salted hash of plain the plain text password</returns>
    public static byte[] GenerateSaltedPbkdf2Hash(string plainTextPassword, byte[] salt)
    {
        return KeyDerivation.Pbkdf2(
            password: plainTextPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
        );
    }
    /// <summary>
    /// Generates a 128 bit long random salt
    /// </summary>
    /// <returns></returns>
    public static byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(128 / 8);
    }

    public static bool IsByteArrayEqual(IEnumerable<byte> expected, IEnumerable<byte> actual)
    {
        return expected.SequenceEqual(actual);
    }
}