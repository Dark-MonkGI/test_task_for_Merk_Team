using BankingInformation.BankingData;

namespace BankingInformation.Credits
{
    internal class EducationCredit : Credit
    {
        public string UniversityName { get; set; }
        public string UniversityAddress { get; set; }

        public override string GetCreditInfo()
        {
            return $"{ID} | {Amount} | {Percent} | {CountOfMonth} | {GetCreditTypeString()} | {Bank.Name} | {Borrower.LastName} {Borrower.FirstName}";
        }

        public EducationCredit(string id, double amount, int countOfMonth, double percent, Borrower borrower, Bank bank,
            string universityName, string universityAddress)
        {
            this.CreditType = CreditType.EducationCredit;
            this.ID = id;
            this.Amount = amount;
            this.CountOfMonth = countOfMonth;
            this.Percent = percent;
            this.Borrower = borrower;
            this.Bank = bank;
            this.UniversityName = universityName;
            this.UniversityAddress = universityAddress;
        }
    }
}
