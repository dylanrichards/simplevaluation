﻿using Newtonsoft.Json;
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
        public DiscountedCashFlow discountedCashFlow = new DiscountedCashFlow();

        private static readonly HttpClient client = new HttpClient();

        public StockModel()
        {

        }

        public StockModel(string ticker)
        {
            StockTicker = ticker;
            ProcessRepositories().Wait();

            this.GrowthRate = 0.05;

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
            var dcfTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/company/discounted-cash-flow/" + StockTicker);
            var waccTask = client.GetStringAsync("https://financialmodelingprep.com/weighted-average-cost-of-capital/" + StockTicker);

            string profileData = await profileTask;
            string incomeData = await incomeTask;
            string enterpriseData = await enterpriseTask;
            string dcfData = await dcfTask;

            string waccData = await waccTask;

            setWACC(waccData);

            this.companyProfile = JsonConvert.DeserializeObject<CompanyProfile>(profileData);
            this.incomeStatement = JsonConvert.DeserializeObject<IncomeStatement>(incomeData);
            this.enterpriseValue = JsonConvert.DeserializeObject<EnterpriseValue>(enterpriseData);
            this.discountedCashFlow = JsonConvert.DeserializeObject<DiscountedCashFlow>(dcfData);
        }

        private void setWACC(string strWACC)
        {
            int index = strWACC.IndexOf("<tr><th>Wacc</th> <td class=\"align-right\"><input type=\"number\" value=\"");
       
            string wacc = strWACC.Substring(index + 70, 5);

            int end = wacc.IndexOf("\"");
            if(end != -1) wacc = wacc.Substring(0, end);

            this.WACC = double.Parse(wacc) / 100;
        }

    }
    
}
