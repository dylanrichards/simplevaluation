using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleValuation.Models
{
    /* https://financialmodelingprep.com/developer/docs#Company-Financial-Statements */
    public class IncomeStatement
    {

        public string symbol { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "${0:N0}", ApplyFormatInEditMode = true)]
        [JsonProperty("revenue")]
        public double Revenue { get; set; }

        [DisplayFormat(DataFormatString = "${0:N0}", ApplyFormatInEditMode = true)]
        [JsonProperty("costOfRevenue")]
        public double CostofRevenue { get; set; }

        [DisplayFormat(DataFormatString = "${0:N0}", ApplyFormatInEditMode = true)]
        [JsonProperty("grossProfit")]
        public double GrossProfit { get; set; }

        [DisplayFormat(DataFormatString = "${0:N2}", ApplyFormatInEditMode = true)]
        [JsonProperty("eps")]
        public double EPS { get; set; }

        
    }
}
