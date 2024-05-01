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
        public CreditType CreditType { get; set; }
        public abstract string GetCreditInfo();


        public virtual string GetCreditTypeString()
        {
            string result = "";

            switch (this.CreditType)
            {
                case CreditType.CarCredit:
                    result = "Car Credit";
                    break;
                case CreditType.Mortgage:
                    result = "Mortgage";
                    break;
                case CreditType.EducationCredit:
                    result = "Education Credit";
                    break;
            }

            return result;
        }
    }
}
