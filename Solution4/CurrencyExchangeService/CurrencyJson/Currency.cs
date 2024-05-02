using System;

namespace CurrencyExchangeService.CurrencyJson
{
    internal class Currency
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Symbol_Native { get; set; }
        public int Decimal_Digits { get; set; }
        public int Rounding { get; set; }
        public string Code { get; set; }
        public string Name_Plural { get; set; }
        public string Type { get; set; }
    }
}
