using System;

namespace CurrencyExchangeService.CurrencyJson
{
    internal class HistoricalExchangeResult
    {
        public Dictionary<string, Dictionary<string, double>> Data { get; set; }
    }
}
