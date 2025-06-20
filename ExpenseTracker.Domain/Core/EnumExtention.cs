namespace ExpenseTracker.Domain.Core;

public static class EnumExtention
{
    public static bool IsInEnum<T>(this T value) where T : Enum
    {
        return !Enum.IsDefined(typeof(T), value);
    }
}
