namespace ExpenseTracker.Domain.Extensions;

public static class StringExtension
{
    public static string ToUsername(this string email)
    {
        var atIndex = email.IndexOf('@');
        return atIndex > 0 ? email[..atIndex] : email;
    }
}
