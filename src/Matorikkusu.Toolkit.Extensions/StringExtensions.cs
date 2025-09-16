using System.Text.RegularExpressions;

namespace Matorikkusu.Toolkit.Extensions;

public static class StringExtensions
{
    public static string IncrementAlphaNumericValue(this string value)
    {
        const string alphaNumericRegularExpression = "^[a-zA-Z0-9]+$";
        
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        if (!Regex.IsMatch(value, alphaNumericRegularExpression)) return string.Empty;
    
        var charArray = value.ToCharArray();
    
        for (var i = charArray.Length - 1; i >= 0; i--)
        {
            var charValue = Convert.ToInt32(charArray[i]);

            if (charValue is 57 or 90 or 122) continue;
            charArray[i]++;
    
            for (var resetIndex = i + 1; resetIndex < charArray.Length; resetIndex++)
            {
                charValue = Convert.ToInt32(charArray[resetIndex]);
                charArray[resetIndex] = charValue switch
                {
                    >= 65 and <= 90 => 'A',
                    >= 97 and <= 122 => 'a',
                    >= 48 and <= 57 => '0',
                    _ => charArray[resetIndex]
                };
            }
    
            return new string(charArray);
        }
    
        return string.Empty;
    }
}