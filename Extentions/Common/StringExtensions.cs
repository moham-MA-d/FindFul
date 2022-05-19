namespace Extensions.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string input)
        {
            if (string.IsNullOrEmpty(input) && string.IsNullOrWhiteSpace(input))
                return true;
            
            return false;
        }
    }
}
