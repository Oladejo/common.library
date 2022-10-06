namespace common.library
{
    public class BankNippsCodeMapper
    {
        public static List<BanksCode> BanksCodes => new List<BanksCode>
        {
                new BanksCode
                {
                    BankName = "MICROFINANCE BANK",
                    CBNCode = "000",
                    NibbsCode = "000000"
                },
                new BanksCode
                {
                    BankName = "SS Bank",
                    CBNCode = "000",
                    NibbsCode = "000000"
                }
            };

        public static BanksCode GetBankViaNibbsCode(string nibbsCode) => BanksCodes.FirstOrDefault(c => c.NibbsCode == nibbsCode);

        public static BanksCode GetBankViaCBNCode(string cbnCode) => BanksCodes.FirstOrDefault(c => c.CBNCode == cbnCode);

        public static string GetBankReference(string reference, string rootRef) => $"{rootRef}-{reference}";
    }

    public class BanksCode
    {
        public string? BankName { get; set; }
        public string? CBNCode { get; set; }
        public string? NibbsCode { get; set; }
    }
}
