using Newtonsoft.Json;
using System.Collections.Generic;
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

        public CompanyProfile companyProfile;
        public List<IncomeStatement> incomeStatements;
        public CashFlowStatement cashFlowStatement;
        public EnterpriseValue enterpriseValue;
        public DiscountedCashFlow discountedCashFlow;

        private static readonly HttpClient client = new HttpClient();

        public StockModel()
        {

        }

        public StockModel(string ticker, double growthRate)
        {
            StockTicker = ticker;

            ProcessRepositories().Wait();

            this.GrowthRate = growthRate / 100.0;

            if (this.cashFlowStatement.FreeCashFlow > 0 && this.WACC > this.GrowthRate)
                this.Valuation = (this.cashFlowStatement.FreeCashFlow / (this.WACC - this.GrowthRate)) / this.enterpriseValue.NumberofShares;
            else
                this.Valuation = discountedCashFlow.DCF;
        }

        private async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            
            var profileTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/profile/" + StockTicker + "?apikey=" + Startup.FMF_APIKEY);
            var incomeTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/income-statement/" + StockTicker + "?limit=4&apikey=" + Startup.FMF_APIKEY);
            var cashFlowTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/cash-flow-statement/" + StockTicker + "?limit=1&apikey=" + Startup.FMF_APIKEY);
            var enterpriseTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/enterprise-values/" + StockTicker + "?apikey=" + Startup.FMF_APIKEY);
            var dcfTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/discounted-cash-flow/" + StockTicker + "?apikey=" + Startup.FMF_APIKEY);
            var waccTask = client.GetStringAsync("https://financialmodelingprep.com/weighted-average-cost-of-capital/" + StockTicker + "?apikey=" + Startup.FMF_APIKEY);

            string profileData = await profileTask;
            string incomeData = await incomeTask;
            string cashFlowData = await cashFlowTask;
            string enterpriseData = await enterpriseTask;
            string dcfData = await dcfTask;
            string waccData = await waccTask;

            setWACC(waccData);

            this.companyProfile = JsonConvert.DeserializeObject<List<CompanyProfile>>(profileData)[0];
            this.incomeStatements = JsonConvert.DeserializeObject<List<IncomeStatement>>(incomeData);
            this.cashFlowStatement = JsonConvert.DeserializeObject<List<CashFlowStatement>>(cashFlowData)[0];
            this.enterpriseValue = JsonConvert.DeserializeObject<List<EnterpriseValue>>(enterpriseData)[0];
            this.discountedCashFlow = JsonConvert.DeserializeObject<List<DiscountedCashFlow>>(dcfData)[0];
        }

        private void setWACC(string strWACC)
        {
            int index = strWACC.IndexOf("<tr><th>Wacc</th> <td class=\"align-right\"><input type=\"number\" id=\"wacc\" value=\"");
       
            string wacc = strWACC.Substring(index + 80, 5);

            int end = wacc.IndexOf("\"");
            if(end != -1) wacc = wacc.Substring(0, end);

            this.WACC = double.Parse(wacc) / 100;
        }

    }
    
}
