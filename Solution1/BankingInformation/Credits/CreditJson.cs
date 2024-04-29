using BankingInformation.BankingData;

namespace BankingInformation.Credits
{
    internal class CreditJson
    {
        public string ID { get; set; }
        public double Amount { get; set; }
        public int CountOfMonth { get; set; }
        public double Percent { get; set; }

        public Borrower Borrower { get; set; }
        public Bank Bank { get; set; }

        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string VIN { get; set; }

        public string AddressOfObject { get; set; }
        public double? Square { get; set; }

        public string UniversityName { get; set; }
        public string UniversityAddress { get; set; }
    }
}
