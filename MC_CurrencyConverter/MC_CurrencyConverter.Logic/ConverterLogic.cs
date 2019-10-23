using MC_CurrencyConverter.Classes;
using System;
using System.Collections.Generic;

namespace MC_CurrencyConverter.Logic
{
    public class ConverterLogic
    {
        public Price ConvertCurrency(List<ExchangeRate> exchangeRates,
                                        List<Product> products,
                                        string targetCurrency, string productName)
        {

            Product product = GetProduct(productName, products);
            ExchangeRate exchangeRate = GetExchangeRate(product.Currency, exchangeRates);

            if (targetCurrency == "EUR")
            {
                return CurrencyToEuro(product, exchangeRate);  //Euro to USD
            }
            else
            {
                Price price = CurrencyToEuro(product, exchangeRate);   //  Usd to Euro
                product.Currency = targetCurrency;
                product.Price = price.price;
                exchangeRate = GetExchangeRate(product.Currency, exchangeRates);//errro
                return EuroToCurrency(product, exchangeRate);
                //Euro to CHF
            }

        }

        private Price EuroToCurrency(Product product, ExchangeRate exchangeRate)
        {
            if (product.Currency == "EUR")
            {
                return new Price(Decimal.Round((product.Price), 2));
            }
            return new Price(Decimal.Round((product.Price * exchangeRate.Rate), 2));

        }

        private Price CurrencyToEuro(Product product, ExchangeRate exchangeRate)
        {
            if (product.Currency.Equals("EUR"))
            {
                return new Price(Decimal.Round(product.Price, 2));
            }
            return new Price(Decimal.Round((product.Price / exchangeRate.Rate), 2));

        }


        private Product GetProduct(string productName, List<Product> products)
        {
            foreach (var product in products)
            {
                if (product.Description.Equals(productName))
                {
                    return product;
                }
            }
            return products[0];
            throw new System.ArgumentException("No Products found");
        }

        private ExchangeRate GetExchangeRate(string targetCurrency, List<ExchangeRate> exchangeRates)
        {
            if (targetCurrency == "EUR")
            {
                return new ExchangeRate("EUR", (decimal)1.00);
            }
            foreach (var rate in exchangeRates)
            {
                if (rate.Currency.ToUpper().Equals(targetCurrency.ToUpper()))
                {
                    return rate;
                }
            }
            throw new System.ArgumentException("No ExchangeRates found");
        }
    }
}
