using System.Globalization;

namespace Infrastructure.Extensions;

public static class DecimalExtension
{
    public static bool ExceededMaxNumbersAfterDot(this decimal value, int maxNumbersAfterDot)
    {
        return value.GetDigitsAfterDot() > maxNumbersAfterDot;
    }
    
    private static int GetDigitsAfterDot(this decimal value)
    {
        var number = value.ToString(CultureInfo.CurrentCulture);
        
        var length = number.Substring(number.IndexOf(".", StringComparison.Ordinal)).Length;

        return length;
    }
}