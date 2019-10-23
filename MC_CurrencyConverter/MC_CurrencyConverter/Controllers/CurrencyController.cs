using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MC_CurrencyConverter.Classes;
using MC_CurrencyConverter.Logic;
using MC_CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MC_CurrencyConverter.Controllers
{

    [ApiController]
    [Route("/api")]
    public class CurrencyController : ControllerBase
    {

        /*
        [MinLength(5)]
        [MaxLength(50)]
        [Required]
        public string Description { get; set; }

        [MaxLength(50)]
        public string AssignedTo { get; set; }
        */

        private static readonly HttpClient HttpClient
           = new HttpClient();
        readonly string exchangeUrl =
            "https://cddataexchange.blob.core.windows.net/data-exchange/htl-homework/ExchangeRates.csv";
        readonly string productUrl =
            "https://cddataexchange.blob.core.windows.net/data-exchange/htl-homework/Prices.csv";



        [HttpGet]
        [Route("products/{product}/price")]
        public async Task<IActionResult> GetAllItemsSortedAsync([FromQuery]string targetCurrency, string product)
        {
            var parser = new DataParser();
            ConverterLogic logic = new ConverterLogic();

            targetCurrency = targetCurrency.ToUpper();
            string exchangeData = await parser.GetFileAsync(exchangeUrl, HttpClient);
            string productData = await parser.GetFileAsync(productUrl, HttpClient);
            List<ExchangeRate> exchangeRates = parser.ExchangeRateToList(exchangeData, ',', 1);
            List<Product> products = parser.ProductsToList(productData, ',', 1);

            Price price = logic.ConvertCurrency(exchangeRates, products, targetCurrency, product);

            var output = JsonConvert.SerializeObject(price);

            return Ok(output);
       
        }


    }

    /*
                .Replace("\r", string.Empty)
                .Split('\n'); ;


    /*private double Round(double value, int digits)
        {
            return Math.Round(value, 2, MidpointRounding.ToEven);
    }*/

    }

