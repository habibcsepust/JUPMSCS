namespace HallManagement.Web.Classes
{
    public static class Utility
    {
        private static String[] units = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static String[] tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        public static String ConvertNumberToWords(Int64 number)
        {
            if (number < 20)
            {
                return units[number];
            }
            if (number < 100)
            {
                return tens[number / 10] + ((number % 10 > 0) ? " " + ConvertNumberToWords(number % 10) : "");
            }
            if (number < 1000)
            {
                return units[number / 100] + " hundred"
                        + ((number % 100 > 0) ? "  " + ConvertNumberToWords(number % 100) : "");
            }
            if (number < 100000)
            {
                return ConvertNumberToWords(number / 1000) + " thousand "
                + ((number % 1000 > 0) ? " " + ConvertNumberToWords(number % 1000) : "");
            }
            if (number < 10000000)
            {
                return ConvertNumberToWords(number / 100000) + " lakh "
                        + ((number % 100000 > 0) ? " " + ConvertNumberToWords(number % 100000) : "");
            }
            if (number < 10000000000)
            {
                return ConvertNumberToWords(number / 10000000) + " crore "
                        + ((number % 10000000 > 0) ? " " + ConvertNumberToWords(number % 10000000) : "");
            }
            return ConvertNumberToWords(number / 1000000000) + " arab "
                    + ((number % 1000000000 > 0) ? " " + ConvertNumberToWords(number % 1000000000) : "");
        }

        public static string GetAmountInWords(string amount)
        {
            string amountString = "";
            string takaPart = "";
            string paisaPart = "";
            if (amount.Contains("."))
            {
                takaPart = amount.Split('.').First();
                paisaPart = amount.Split('.').Last();
            }

            amountString = Utility.ConvertNumberToWords(Int64.Parse(takaPart)) + " Taka";
            if (paisaPart != "" && paisaPart != "00")
                amountString += Utility.ConvertNumberToWords(Int64.Parse(paisaPart)) + " Paisa";

            return amountString;
        }
    }
}
