using BankingInformation.BankingData;

namespace BankingInformation.Credits
{
    internal abstract class Credit
    {
        public string ID { get; set; }
        public double Amount { get; set; }
        public int CountOfMonth { get; set; }
        public double Percent { get; set; }

        public Borrower Borrower { get; set; }
        public Bank Bank { get; set; }

        public abstract string GetCreditInfo();
        public abstract string GetCreditType();
    }
}
