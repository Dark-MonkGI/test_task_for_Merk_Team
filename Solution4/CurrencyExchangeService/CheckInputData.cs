using System;
using System.Globalization;

namespace CurrencyExchangeService
{
    internal class CheckInputData
    {
        private HashSet<string> currencyCodes;

        public CheckInputData(ExchangeService exchangeService)
        {
            this.currencyCodes = exchangeService.GetCurrencyCodes().Result;
        }

        public string SafeReadCurrencyCode()
        {
            string userInput = (Console.ReadLine() ?? "").Trim().ToUpper();

            while (string.IsNullOrWhiteSpace(userInput) || !this.currencyCodes.Contains(userInput))
            {
                Console.WriteLine("\nIncorrect currency code. Please try again: ");
                userInput = (Console.ReadLine() ?? "").Trim().ToUpper();
            }

            return userInput;
        }

        public double SafeReadDouble()
        {
            double result;

            while (true)
            {
                string? input = Console.ReadLine();
                input = input?.Replace(',', '.'); 

                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                {
                    break;  
                }

                Console.WriteLine("Invalid input. Please try again:");
            }

            return result;
        }

        public string ConvertUserInputToDate()
        { 
            DateTime parsedDate;
            string[] formats = { "yyyy-MM-dd", "dd.MM.yyyy", "dd,MM,yyyy", "dd/MM/yyyy", "yyyy/MM/dd", "yyyy.MM.dd" };

            Console.WriteLine("Enter a date: ");
            string? userInputData = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(userInputData)
                    || !DateTime.TryParseExact(userInputData, formats,
                                               CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out parsedDate))
            {
                Console.WriteLine("Invalid input or format. Please enter the date again: ");
                userInputData = Console.ReadLine();
            }

            return parsedDate.ToString("yyyy-MM-dd");
        }
    }
}
