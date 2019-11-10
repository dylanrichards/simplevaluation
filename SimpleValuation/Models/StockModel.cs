using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleValuation.Models
{
    public class StockModel
    {

        public string StockTicker { get; set; }
        public double CurrentPrice { get; set; }
        public string CompanyName { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyLogo { get; set; }

        private static readonly HttpClient client = new HttpClient();

        public StockModel()
        {

        }

        public StockModel(string ticker)
        {
            StockTicker = ticker;
            ProcessRepositories().Wait();
        }

        private async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            var stringTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/company/profile/" + StockTicker);

            string msg = await stringTask;

            dynamic pricing = JsonConvert.DeserializeObject(msg);
            CurrentPrice = Convert.ToDouble(pricing.profile.price);
            CompanyName = pricing.profile.companyName;
            CompanyWebsite = pricing.profile.website;
            CompanyLogo = pricing.profile.image;
        }

    }
    
}
