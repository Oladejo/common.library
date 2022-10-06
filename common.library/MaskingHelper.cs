using System.Text;

namespace common.library
{
    public static class MaskingHelper
    {
        public static string MaskedAccount(string accountNumber)
        {
            int length = accountNumber.Length;
            string firstPart = accountNumber.Substring(0, 3);
            string midPart = accountNumber.Substring(3, length - 6);
            string lastPart = accountNumber.Substring(length - 3);

            int count = 0;
            StringBuilder masked = new StringBuilder();
            while (count < midPart.Length)
            {
                masked.Append('*');
                count++;
            }

            return firstPart + masked.ToString() + lastPart;
        }
    }
}
