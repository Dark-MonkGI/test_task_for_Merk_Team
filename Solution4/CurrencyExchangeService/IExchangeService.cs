using System;

namespace CurrencyExchangeService
{
    internal interface IExchangeService
    {
        Task GetCurrencies();
        Task Exchange(string from, string to, double amount);
        Task HistoricalExchange(string from, string to, double amount, string date);
    }
}
