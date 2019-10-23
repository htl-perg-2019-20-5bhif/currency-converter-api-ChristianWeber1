using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MC_CurrencyConverter
{
    public class ExchangeRate
    {

        public string Currency;
        public decimal Rate;

        public ExchangeRate(string currency, decimal rate)
        {
            this.Currency = currency.ToUpper();
            this.Rate = rate;
        }
    }
}
