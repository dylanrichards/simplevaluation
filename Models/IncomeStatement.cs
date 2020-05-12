using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
            [JsonProperty("date")]
            public DateTime Date { get; set; }

            [DisplayFormat(DataFormatString = "${0:N0}", ApplyFormatInEditMode = true)]
            [JsonProperty("Revenue")]
            public double Revenue { get; set; }

            [DisplayFormat(DataFormatString = "${0:N0}", ApplyFormatInEditMode = true)]
            [JsonProperty("Cost of Revenue")]
            public double CostofRevenue { get; set; }

            [DisplayFormat(DataFormatString = "${0:N0}", ApplyFormatInEditMode = true)]
            [JsonProperty("Gross Profit")]
            public double GrossProfit { get; set; }

            [DisplayFormat(DataFormatString = "${0:N2}", ApplyFormatInEditMode = true)]
            [JsonProperty("EPS")]
            public double EPS { get; set; }

            [JsonProperty("Free Cash Flow margin")]
            public double FCFMargin { get; set; }

            public double FreeCashFlow { get; set; }
        }
    }
}
