using BankingInformation.BankingData;

namespace BankingInformation.Credits
{
    internal class CarCredit : Credit
    {
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public string VIN { get; set; }


        public override string GetCreditInfo()
        {
            return $"{ID} | {Amount} | {Percent} | {CountOfMonth} | {GetCreditTypeString()} | {Bank.Name} | {Borrower.LastName} {Borrower.FirstName}";
        }

        public CarCredit(string id, double amount, int countOfMonth, double percent, Borrower borrower, Bank bank, 
            string carModel, string carBrand, string vin)
        {
            this.CreditType = CreditType.CarCredit;
            this.ID = id;
            this.Amount = amount;
            this.CountOfMonth = countOfMonth;
            this.Percent = percent;
            this.Borrower = borrower;
            this.Bank = bank;
            this.CarModel = carModel;
            this.CarBrand = carBrand;
            this.VIN = vin;
        }
    }
}
