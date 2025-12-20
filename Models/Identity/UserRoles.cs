using System;

namespace GuardingChild.Models.Identity;

public static class UserRoles
{
    public const string Doctor = "Doctor";
    public const string Police = "Police";

    public static bool TryNormalize(string? role, out string normalizedRole)
    {
        normalizedRole = string.Empty;
        if (string.IsNullOrWhiteSpace(role)) return false;

        if (string.Equals(role, Doctor, StringComparison.OrdinalIgnoreCase))
        {
            normalizedRole = Doctor;
            return true;
        }

        if (string.Equals(role, Police, StringComparison.OrdinalIgnoreCase))
        {
            normalizedRole = Police;
            return true;
        }

        return false;
    }
}
