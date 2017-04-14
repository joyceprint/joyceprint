namespace Analytics
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return null == str || str.Trim().Length == 0;
        }
    }
}