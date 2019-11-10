using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleValuation.Models
{
    public class IncomeStatement
    {
        public string symbol { get; set; }

        public Financials[] financials;

        public class Financials
        {
            [JsonProperty("Revenue")]
            public double Revenue { get; set; }

            [JsonProperty("Cost of Revenue")]
            public double CostofRevenue { get; set; }

            [JsonProperty("Gross Profit")]
            public double GrossProfit { get; set; }

            [JsonProperty("EPS")]
            public double EPS { get; set; }
        }
    }
}
