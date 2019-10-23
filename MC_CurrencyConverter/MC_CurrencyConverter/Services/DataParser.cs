using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MC_CurrencyConverter.Services
{
    public class DataParser
    {
        public List<Product> ProductsToList(string dataString, char seperator, int ignoreLines)
        {
            var products = new List<Product>();
            string[] data = dataString
                .Replace("\r", string.Empty)
                .Split('\n');

            string[] line;
            decimal rate;
            for (int i = ignoreLines; i < data.Count(); i++)
            {
                line = data[i].Split(seperator);

                if (line.Count() == 3)
                {

                    rate = decimal.Parse(line[2], System.Globalization.CultureInfo.InvariantCulture);
                    products.Add(new Product(line[0], line[1], rate));
                }
            }

            return products;
        }

        public List<ExchangeRate> ExchangeRateToList(string dataString, char seperator, int ignoreLines)
        {
            var rates = new List<ExchangeRate>();
            string[] data = dataString
                .Replace("\r", string.Empty)
                .Split('\n');

            string[] line;
            decimal price;
            for (int i = ignoreLines; i < data.Count(); i++)
            {
                line = data[i].Split(seperator);

                if (line.Count() == 2)
                {
                    price = decimal.Parse(line[1], System.Globalization.CultureInfo.InvariantCulture);
                    rates.Add(new ExchangeRate(line[0], price));
                }
            }

            return rates;

        }

        public async Task<string> FileToString(string fileName)
        {
            string content;
            try
            {
                content = await System.IO.File.ReadAllTextAsync(fileName);
            }
            catch (FileNotFoundException ex)
            {
                throw new System.ArgumentException("File does not exist!  " + ex.Message);
                throw;
            }

            return content;
        }

        public async Task<string> GetFileAsync(string url, HttpClient HttpClient)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string ret = await response.Content.ReadAsStringAsync();

            return ret;
        }
    }
}
