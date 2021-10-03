using Newtonsoft.Json;

namespace SimpleValuation.Models
{
    /* https://financialmodelingprep.com/developer/docs#Company-Discounted-cash-flow-value */
    public class DiscountedCashFlow
    {

        [JsonProperty("dcf")]
        public double DCF { get; set; }

    }
}
