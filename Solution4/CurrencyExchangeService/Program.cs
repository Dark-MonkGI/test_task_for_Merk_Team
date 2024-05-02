using System;

namespace CurrencyExchangeService
{
    internal class Program
    {
        private static ExchangeService exchangeService = new ExchangeService();

        static async Task Main(string[] args)
        {
            CheckInputData checkInputData = new CheckInputData(exchangeService);

            while (true)
            {
                Console.WriteLine("Enter the choice:");
                Console.WriteLine("1. Get all currencies");
                Console.WriteLine("2. Convert from one currency to another");
                Console.WriteLine("3. Convert from one currency to another on a specific date in the past");
                Console.WriteLine("0. Exit");

                var input = Console.ReadLine();

                string from, to, date;
                double amount;

                switch (input)
                {
                    case "1":
                        await exchangeService.GetCurrencies();
                        break;
                    case "2":
                        Console.WriteLine("\nEnter the from currency code:");
                        from = checkInputData.SafeReadCurrencyCode();

                        Console.WriteLine("\nEnter the to currency code:"); 
                        to = checkInputData.SafeReadCurrencyCode();

                        Console.WriteLine("\nEnter the amount to convert:");
                        amount = checkInputData.SafeReadDouble();

                        await exchangeService.Exchange(from, to, amount);
                        break;
                    case "3":
                        Console.WriteLine("\nEnter the from currency code:");
                        from = checkInputData.SafeReadCurrencyCode();

                        Console.WriteLine("\nEnter the to currency code:");
                        to = checkInputData.SafeReadCurrencyCode();

                        Console.WriteLine("\nEnter the amount to convert:");
                        amount = checkInputData.SafeReadDouble();

                        Console.WriteLine("\nEnter the date of conversion (YYYY-MM-DD):");
                        date = checkInputData.ConvertUserInputToDate();

                        await exchangeService.HistoricalExchange(from, to, amount, date);
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
