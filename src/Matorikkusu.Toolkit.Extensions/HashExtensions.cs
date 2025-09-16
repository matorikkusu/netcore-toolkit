using System.Security.Cryptography;
using System.Text;

namespace Matorikkusu.Toolkit.Extensions;

public static class HashExtensions
{
    public static string Sha256(this string input)
    {
        if (input.IsMissing()) return string.Empty;

        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

    public static byte[] Sha256(this byte[] input)
    {
        if (input == null)
        {
            return null;
        }

        using var sha = SHA256.Create();
        return sha.ComputeHash(input);
    }

    public static string Sha512(this string input)
    {
        if (input.IsMissing()) return string.Empty;

        using var sha = SHA512.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

    private static bool IsMissing(this string input)
    {
        return string.IsNullOrWhiteSpace(input);
    }
}