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

        public CompanyProfile companyProfile = new CompanyProfile();
        public IncomeStatement incomeStatement = new IncomeStatement();
        
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
            
            var profileTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/company/profile/" + StockTicker);
            var incomeTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/financials/income-statement/" + StockTicker);

            string profileData = await profileTask;
            string incomeData = await incomeTask;

            this.companyProfile = JsonConvert.DeserializeObject<CompanyProfile>(profileData);
            this.incomeStatement = JsonConvert.DeserializeObject<IncomeStatement>(incomeData);
        }

    }
    
}
