using BankingInformation.BankingData;
using BankingInformation.Credits;
using System.Globalization;


namespace BankingInformation
{
    internal class CreditInformationService
    {
        /// <summary>
        /// Displays information about all active loans
        /// </summary>
        /// <param name="credits"></param>
        public void GetListOfAllCredits(List<Credit> credits)
        {
            string tableHeader = "\nLoan ID | Loan amount | Interest rate | Loan term | Loan type | Bank name | " +
                "Borrower's surname and first name";

            Console.WriteLine(tableHeader);

            foreach (var credit in credits)
            {
                Console.WriteLine(credit.GetCreditInfo());
            }
        }

        /// <summary>
        /// Displays a list of all unique banks
        /// </summary>
        /// <param name="credits"></param>
        public void GetListOfAllBanks(List<Credit> credits)
        {
            List<Bank> banks = credits.Select(credit => credit.Bank).Distinct().ToList();

            Console.WriteLine("\nList of all banks: ");
            foreach (var bank in banks)
            {
                Console.WriteLine(bank.Name);
            }
        }

        /// <summary>
        /// Displays a list of all unique borrowers
        /// </summary>
        /// <param name="credits"></param>
        public void GetListOfAllBorrowers(List<Credit> credits)
        {
            List<Borrower> borrowers = credits.Select(credit => credit.Borrower).Distinct().ToList();

            Console.WriteLine("\nList of all borrowers: ");
            foreach (var borrower in borrowers)
            {
                Console.WriteLine($"{borrower.FirstName} {borrower.LastName}");
            }
        }

        /// <summary>
        /// Displays information on all loans according to the selected loan type
        /// </summary>
        /// <param name="credits"></param>
        public void GetListCreditsByType(List<Credit> credits)
        {
            Console.WriteLine("\nSelect the type of loan you are interested in: ");
            Console.WriteLine("1. Car Credit");
            Console.WriteLine("2. Mortgage");
            Console.WriteLine("3. Education Credit");
            Console.WriteLine("0. Back to main menu");


            string userChoice = Console.ReadLine()!;
            string creditType;

            switch (userChoice)
            {
                case "1":
                    creditType = "Car Credit";
                    break;
                case "2":
                    creditType = "Mortgage";
                    break;
                case "3":
                    creditType = "Education Credit";
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nInvalid choice, please try again!\n");
                    return;
            }

            List<Credit> creditsOfType = credits.Where(credit => credit.GetCreditType() == creditType).ToList();

            Console.WriteLine($"\nList of loans for the selected type - {creditType}: ");

            string tableHeader = "\nLoan ID | Loan amount | Interest rate | Loan term | Loan type | Bank name | " +
                    "Borrower's surname and first name";
            Console.WriteLine(tableHeader);

            foreach (var creditOfType in creditsOfType)
            {
                Console.WriteLine($"{creditOfType.GetCreditInfo()}");
            }
        }

        /// <summary>
        /// The method queries the user for loan data and creates a new loan record
        /// </summary>
        /// <param name="credits"></param>
        public void AddNewCredit(List<Credit> credits)
        {
            string userChoice;

            while (true)
            {
                Console.WriteLine("\nSelect which type of credit you want to add: ");
                Console.WriteLine("1. Car Credit");
                Console.WriteLine("2. Mortgage");
                Console.WriteLine("3. Education Credit");
                Console.WriteLine("0. Back to main menu");
                userChoice = SafeReadLine();

                switch (userChoice)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "0":
                        return; 
                    default:
                        Console.WriteLine("\nInvalid choice, please try again!\n");
                        continue; 
                }

                break;
            }


            Console.WriteLine("\nEnter the ID of the credit: ");
            string id = SafeReadLine();

            double amount;
            Console.WriteLine("\nEnter the amount of the credit: ");
            while (!double.TryParse(SafeReadLine().Replace('.', ','), out amount))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            int countOfMonth;
            Console.WriteLine("\nEnter the credit term in months: ");
            while (true)
            {
                string userInput = SafeReadLine();
                if (int.TryParse(userInput, out countOfMonth) && countOfMonth > 1)
                {
                    break; 
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer between 1 and 12.");
                }
            }

            double percent;
            Console.WriteLine("\nEnter the percentage rate: ");
            while (!double.TryParse(SafeReadLine().Replace('.', ','), out percent))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }


            Console.WriteLine("\n\n----------------------");
            Console.WriteLine("Enter bank details: ");
            Console.WriteLine("Enter the bank ID:");
            string bankId = SafeReadLine();

            Console.WriteLine("\nEnter the bank name:");
            string bankName = SafeReadLine();

            Console.WriteLine("\nEnter the bank address:");
            string bankAddress = SafeReadLine();

            Bank bank = new Bank(bankId, bankName, bankAddress);


            Console.WriteLine("\n\n----------------------");
            Console.WriteLine("Enter borrower details: ");
            Console.WriteLine("Enter the borrower ID: ");
            string borrowerId = SafeReadLine();

            Console.WriteLine("\nEnter the borrower first name: ");
            string borrowerFirstName = SafeReadLine();

            Console.WriteLine("\nEnter the borrower last name: ");
            string borrowerLastName = SafeReadLine();

            Console.WriteLine("\nEnter the borrower date of birth (dd/MM/yyyy): ");
            DateTime borrowerDateOfBirth;
            while (!DateTime.TryParseExact(SafeReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out borrowerDateOfBirth))
            {
                Console.WriteLine("Incorrect date format! Please enter the correct date in the format (dd/MM/yyyy): ");
            }

            Console.WriteLine("Enter the borrower passport number: ");
            string borrowerPassportNumber = SafeReadLine();

            Borrower borrower = new Borrower(borrowerId, borrowerFirstName, borrowerLastName, borrowerDateOfBirth, 
                borrowerPassportNumber);

            Credit newCredit;

            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("Enter car model: ");
                    string carModel = SafeReadLine();
                    Console.WriteLine("Enter car brand: ");
                    string carBrand = SafeReadLine();
                    Console.WriteLine("Enter car vin: ");
                    string vin = SafeReadLine();

                    newCredit = new CarCredit(id, amount, countOfMonth, percent, borrower, bank, carModel, carBrand, vin);
                    break;
                case "2":
                    Console.WriteLine("Enter address: ");
                    string addressOfObject = SafeReadLine();

                    double square;
                    Console.WriteLine("Enter square: ");
                    while (!double.TryParse(SafeReadLine().Replace('.', ','), out square))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }

                    newCredit = new Mortgage(id, amount, countOfMonth, percent, borrower, bank, addressOfObject, square);
                    break;
                case "3":
                    Console.WriteLine("Enter university name: ");
                    string universityName = SafeReadLine();
                    Console.WriteLine("Enter university address: ");
                    string universityAddress = SafeReadLine();

                    newCredit = new EducationCredit(id, amount, countOfMonth, percent, borrower, bank, universityName, universityAddress);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nInvalid choice, please try again!\n");
                    return;
            }


            credits.Add(newCredit);

            Console.WriteLine("\nNew credit added successfully!");
        }

        /// <summary>
        /// Reads a non-null and non-whitespace line from the Console
        /// </summary>
        /// <returns>A non-null and non-whitespace string that the user entered</returns>
        private string SafeReadLine()
        {
            string? result = Console.ReadLine();

            while (result == null || string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine("Invalid input. Please try again.");
                result = Console.ReadLine();
            }

            return result;
        }

        /// <summary>
        /// Displays the list of credits associated with the given borrower's surname
        /// </summary>
        /// <param name="credits">The list of credits to search</param>
        public void GetListCreditsByBorrowerSurname(List<Credit> credits)
        {
            Console.WriteLine("\nEnter the borrower's last name: ");
            string borrowerLastName = SafeReadLine();

            List<Credit> creditsForBorrower = credits
                .Where(credit => credit.Borrower.LastName.Equals(borrowerLastName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var creditForBorrower in creditsForBorrower)
            {
                Console.WriteLine(creditForBorrower.GetCreditInfo());
            }
        }

        /// <summary>
        /// Calculates the amount of the monthly payment
        /// </summary>
        /// <param name="credits">Displays the calculated payment amount</param>
        public void CalculateAnnuityPayment(List<Credit> credits)
        {
            Console.WriteLine("\nEnter the credit ID to calculate the monthly annuity payment amount: ");
            string creditID = SafeReadLine();

            Credit? credit = credits.FirstOrDefault(c => c.ID == creditID);

            if (credit != null)
            {
                double monthlyInterestRate = (credit.Percent / 100) / 12;

                double annuityFactor = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, credit.CountOfMonth) / 
                    (Math.Pow(1 + monthlyInterestRate, credit.CountOfMonth) - 1);

                double monthlyPayment = credit.Amount * annuityFactor;

                Console.WriteLine($"\nThe amount of the monthly annuity payment: {Math.Round(monthlyPayment, 2)}£"); 
            }
            else
            {
                Console.WriteLine($"\nNo credit was found for the specified ID.");
            }      
        }
    }
}
