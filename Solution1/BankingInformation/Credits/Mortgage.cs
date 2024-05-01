using BankingInformation.BankingData;

namespace BankingInformation.Credits
{
    internal class Mortgage : Credit
    {
        public string AddressOfObject { get; set; }
        public double Square { get; set; }


        public override string GetCreditInfo()
        {
            return $"{ID} | {Amount} | {Percent} | {CountOfMonth} | {GetCreditTypeString()} | {Bank.Name} | {Borrower.LastName} {Borrower.FirstName}";
        }

        public Mortgage(string id, double amount, int countOfMonth, double percent, Borrower borrower, Bank bank,
            string addressOfObject, double square)
        {
            this.CreditType = CreditType.Mortgage;
            this.ID = id;
            this.Amount = amount;
            this.CountOfMonth = countOfMonth;
            this.Percent = percent;
            this.Borrower = borrower;
            this.Bank = bank;
            this.AddressOfObject = addressOfObject;
            this.Square = square;
        }
    }
}
