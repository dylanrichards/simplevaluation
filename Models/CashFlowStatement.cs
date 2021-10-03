using Newtonsoft.Json;

namespace SimpleValuation.Models
{
    /* https://financialmodelingprep.com/developer/docs#Company-Financial-Statements */
    public class CashFlowStatement
    {

        [JsonProperty("freeCashFlow")]
        public double FreeCashFlow { get; set; }
    }
}
