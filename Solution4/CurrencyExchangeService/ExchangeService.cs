using System;
using System.Net;
using System.Net.Http.Headers;
using CurrencyExchangeService.CurrencyJson;
using Newtonsoft.Json;

namespace CurrencyExchangeService
{
    internal class ExchangeService : IExchangeService
    {
        private readonly HttpClient _httpClient;
        private static readonly string apiKey = "fca_live_xADaHstVYbKOKucqIGcnQ44JgGKWfzUNcidr25eT";

        public ExchangeService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.freecurrencyapi.com/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
        }

        public async Task GetCurrencies()
        {
            try 
            {
                var response = await _httpClient.GetAsync("v1/currencies");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    var root = JsonConvert.DeserializeObject<Root>(jsonString);

                    if (root?.Data == null)
                    {
                        Console.WriteLine("\nNo Data loaded from server");
                    }
                    else
                    {
                        Console.WriteLine($"\n--------------------------------------");
                        Console.WriteLine($"List of available currencies: ");
                        Console.WriteLine($"-------------------");
                        Console.WriteLine($"| Code |   Name   |");
                        Console.WriteLine($"-------------------");

                        foreach (KeyValuePair<string, Currency> kvp in root.Data)
                        {
                            Console.WriteLine($"| {kvp.Key}  | {kvp.Value.Name} |");
                        }
                        Console.WriteLine($"--------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine($"\nError: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }          
        }

        public async Task Exchange(string from, string to, double amount)
        {
            try
            {
                var response = await _httpClient.GetAsync($"v1/latest?base_currency={from}&currencies={to}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var exchangeData = JsonConvert.DeserializeObject<ExchangeResult>(data);

                    if (exchangeData?.Data == null)
                    {
                        Console.WriteLine("No Data loaded from server");
                    }
                    else
                    {
                        double currencyRatio = exchangeData.Data[to];

                        double convertedAmount = amount * currencyRatio;

                        Console.WriteLine($"\n----------------------------");
                        Console.WriteLine($"Converted amount: {Math.Round(convertedAmount, 2)} {to}");
                        Console.WriteLine($"----------------------------\n");
                    }
                }
                else
                {
                    Console.WriteLine($"\nError: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }

        public async Task HistoricalExchange(string from, string to, double amount, string date)
        {
            DateTime dateTime = DateTime.Parse(date);
            string formattedDate = dateTime.ToString("dd.MM.yyyy");

            try
            {
                var response = await _httpClient.GetAsync($"v1/historical?date={date}&base_currency={from}&currencies={to}");
                var data = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
                {
                    Console.WriteLine($"\nThere is no exchange data for the date {formattedDate}\n");
                }
                else if (response.IsSuccessStatusCode)
                {
                    var exchangeData = JsonConvert.DeserializeObject<HistoricalExchangeResult>(data);

                    if (exchangeData?.Data == null)
                    {
                        Console.WriteLine("No data loaded from server");
                        return;
                    }

                    double historicalCurrencyRatio = exchangeData.Data[date][to];
                    double convertedHistoricalAmount = amount * historicalCurrencyRatio;

                    Console.WriteLine($"\n----------------------------");
                    Console.WriteLine($"{formattedDate} converted amount: {Math.Round(convertedHistoricalAmount, 2)} {to}");
                    Console.WriteLine($"----------------------------\n");
                }
                else
                {
                    Console.WriteLine($"\nError: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }

        public async Task<HashSet<string>> GetCurrencyCodes()
        {
            var currencyCodes = new HashSet<string>();

            try
            {
                var response = await _httpClient.GetAsync("v1/currencies");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var root = JsonConvert.DeserializeObject<Root>(jsonString);

                    if (root?.Data != null)
                    {
                        foreach (KeyValuePair<string, Currency> kvp in root.Data)
                        {
                            currencyCodes.Add(kvp.Key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }

            return currencyCodes;
        }
    }
}
