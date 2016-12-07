using System;

namespace Analytics
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this String str)
        {
            return null == str || str.Trim().Length == 0;
        }
    }
}
