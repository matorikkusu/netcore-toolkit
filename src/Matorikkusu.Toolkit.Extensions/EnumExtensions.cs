using System.ComponentModel;

namespace Matorikkusu.Toolkit.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        var attributes =
            (DescriptionAttribute[]) fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

        return attributes.Length > 0 ? attributes[0].Description : enumValue.ToString();
    }

    public static Dictionary<T, int> GetEnumKeyValues<T>()
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .ToDictionary(key => key, value => Convert.ToInt32(value));
    }

    public static IEnumerable<T> GetEnumValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
}