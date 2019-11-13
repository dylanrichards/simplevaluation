using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleValuation.Models
{
    public class StockModel
    {

        public string StockTicker { get; set; }
        public double GrowthRate { get; set; }
        public double WACC { get; set; }

        public double Valuation { get; set; }

        public CompanyProfile companyProfile = new CompanyProfile();
        public IncomeStatement incomeStatement = new IncomeStatement();
        public EnterpriseValue enterpriseValue = new EnterpriseValue();

        private static readonly HttpClient client = new HttpClient();

        public StockModel()
        {

        }

        public StockModel(string ticker)
        {
            StockTicker = ticker;
            ProcessRepositories().Wait();

            this.GrowthRate = 0.05;
            this.WACC = 0.1005;
            this.incomeStatement.financials[0].FreeCashFlow = this.incomeStatement.financials[0].FCFMargin * this.incomeStatement.financials[0].Revenue;
            this.Valuation = this.incomeStatement.financials[0].FreeCashFlow / (this.WACC - this.GrowthRate);

            this.Valuation = this.Valuation / this.enterpriseValue.enterpriseValues[0].NumberofShares;
        }

        private async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            
            var profileTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/company/profile/" + StockTicker);
            var incomeTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/financials/income-statement/" + StockTicker);
            var enterpriseTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/enterprise-value/" + StockTicker);

            string profileData = await profileTask;
            string incomeData = await incomeTask;
            string enterpriseData = await enterpriseTask;

            this.companyProfile = JsonConvert.DeserializeObject<CompanyProfile>(profileData);
            this.incomeStatement = JsonConvert.DeserializeObject<IncomeStatement>(incomeData);
            this.enterpriseValue = JsonConvert.DeserializeObject<EnterpriseValue>(enterpriseData);
        }

    }
    
}
