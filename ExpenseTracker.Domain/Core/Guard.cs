using System.Runtime.CompilerServices;

public static class Guard
{
    public static void AllNotDefault(Exception exToThrow, params object[] values)
    {
        var length = values.Length;
        for (var index = 0; index < length; ++index) NotDefault(values[index], exToThrow);
    }

    public static void AreEqual(object obj1, object obj2, Exception exToThrow)
    {
        if (!EqualityComparer<object>.Default.Equals(obj1, obj2))
        {
            throw exToThrow;
        }
    }

    public static void IsEmpty<T>(IEnumerable<T> value, Exception exToThrow)
    {
        if (value.Any())
        {
            throw exToThrow;
        }
    }

    public static void IsFalse(bool value, Exception exToThrow)
    {
        if (value)
        {
            throw exToThrow;
        }
    }

    public static void IsTrue(bool value, Exception exToThrow)
    {
        if (!value)
        {
            throw exToThrow;
        }
    }

    public static void NotDefault<T>(T value, Exception exToThrow)
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
        {
            throw exToThrow;
        }
    }

    public static void NotEmpty<T>(IEnumerable<T> value, Exception exToThrow)
    {
        if (!value.Any())
        {
            throw exToThrow;
        }
    }

    public static void NotEqual<T>(T value1, T value2, Exception exToThrow)
    {
        if (EqualityComparer<T>.Default.Equals(value1, value2))
        {
            throw exToThrow;
        }
    }


    public static T NotNull<T>(T value, Exception exToThrow)
        where T : class?
    {
        if (value == null)
        {
            throw exToThrow;
        }

        return value;
    }

    public static T NotNull<T>(T value, [CallerArgumentExpression("value")] string? paramName = null, string? message = null)
        where T : class?
    {
        var ex = message == null
            ? new ArgumentNullException(paramName)
            : new ArgumentException(message, paramName);

        return NotNull(value, ex);
    }

    public static T NotNull<T>(T? value, Exception exToThrow)
        where T : struct
    {
        if (value == null)
        {
            throw exToThrow;
        }

        return value.Value;
    }
}