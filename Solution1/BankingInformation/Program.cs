using BankingInformation.Credits;
using Newtonsoft.Json;

namespace BankingInformation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please specify the correct path to the JSON file as a parameter");
                return;
            }

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File does not exist. Specified file path: {filePath}");
            }
            else
            {
                string jsonString;
                try
                {
                    jsonString = File.ReadAllText(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                    return;
                }

                List<CreditJson>? creditsJson;
                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        DateFormatString = "dd/MM/yyyy"
                    };

                    creditsJson = JsonConvert.DeserializeObject<List<CreditJson>>(jsonString, settings);

                    if (creditsJson == null)
                    {
                        Console.WriteLine("Couldn't parse json file, it may be corrupt.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while deserializing the JSON: {ex.Message}");
                    return;
                }


                List<Credit> credits = new List<Credit>();
                foreach (var creditJson in creditsJson)
                {
                    if (!string.IsNullOrEmpty(creditJson.CarBrand))
                    {
                        var carCredit = new CarCredit(
                            creditJson.ID,
                            creditJson.Amount,
                            creditJson.CountOfMonth,
                            creditJson.Percent,
                            creditJson.Borrower,
                            creditJson.Bank,
                            creditJson.CarBrand,
                            creditJson.CarModel,
                            creditJson.VIN
                        );
                        credits.Add(carCredit);
                    }
                    else if (!string.IsNullOrEmpty(creditJson.AddressOfObject))
                    {
                        var mortgage = new Mortgage(
                            creditJson.ID,
                            creditJson.Amount,
                            creditJson.CountOfMonth,
                            creditJson.Percent,
                            creditJson.Borrower,
                            creditJson.Bank,
                            creditJson.AddressOfObject,
                            creditJson.Square ?? default(double)
                        );
                        credits.Add(mortgage);
                    }
                    else if (!string.IsNullOrEmpty(creditJson.UniversityName))
                    {
                        var educationCredit = new EducationCredit(
                            creditJson.ID,
                            creditJson.Amount,
                            creditJson.CountOfMonth,
                            creditJson.Percent,
                            creditJson.Borrower,
                            creditJson.Bank,
                            creditJson.UniversityName,
                            creditJson.UniversityAddress
                        );
                        credits.Add(educationCredit);
                    }
                }


                // -Получить список всех кредитов вида:
                //     ID кредита | Сумма кредита | Процентная ставка | Срок кредита | Тип кредита | Наименование банка | 
                //         Фамилия и Имя заемщика
                // - Получить список всех банков;
                // - Получить список всех заемщиков;
                // - Получить список кредитов по заданному типу(автокредит, ипотека или кредит на образование);
                // - Добавить новый кредит;
                // - Получить список кредитов по заданной фамилии заемщика.
                // - Рассчитать сумму ежемесячного аннуитетного платежа для заданного кредита(по ID кредита).
                Console.WriteLine("Welcome to the credit system!");

                CreditInformationService creditInfoService = new CreditInformationService();

                while (true)
                {
                    Console.WriteLine("\nAvailable options:");
                    Console.WriteLine("1. Get list of all credits.");
                    Console.WriteLine("2. Get list of all banks.");
                    Console.WriteLine("3. Get list of all borrowers.");
                    Console.WriteLine("4. Get list of credits by type.");
                    Console.WriteLine("5. Add new credit.");
                    Console.WriteLine("6. Get list of credits by borrower's surname.");
                    Console.WriteLine("7. Calculate annuity payment for the specified credit.");
                    Console.WriteLine("0. Exit");
                    Console.WriteLine("\nPlease, enter the number of the operation you want to perform: \n");

                    string userChoice = Console.ReadLine()!;

                    switch (userChoice)
                    {
                        case "1":
                            creditInfoService.GetListOfAllCredits(credits);
                            break;
                        case "2":
                            creditInfoService.GetListOfAllBanks(credits);
                            break;
                        case "3":
                            creditInfoService.GetListOfAllBorrowers(credits);
                            break;
                        case "4":
                            creditInfoService.GetListCreditsByType(credits);
                            break;
                        case "5":
                            creditInfoService.AddNewCredit(credits);
                            break;
                        case "6":
                            creditInfoService.GetListCreditsByBorrowerSurname(credits);
                            break;
                        case "7":
                            creditInfoService.CalculateAnnuityPayment(credits);
                            break;
                        case "0":
                            Console.WriteLine("\nGoodbye!\n");
                            return;
                        default:
                            Console.WriteLine("\nInvalid choice, please try again!\n");
                            break;
                    }
                }
            }
        }
    }
}
