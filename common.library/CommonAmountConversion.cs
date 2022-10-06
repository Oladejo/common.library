namespace common.library
{
    public static class CommonAmountConversion
    {
        public static string ConvertToString(decimal amount)
        {
            return ((int)(amount * 100)).ToString();
        }

        public static string ConvertToString(float amount)
        {
            return ((int)(amount * 100)).ToString();
        }

        public static string RemoveDecimal(string amount)
        {
            var newAmount = decimal.Parse(amount);
            return ((int)newAmount).ToString();
        }

    }
}
