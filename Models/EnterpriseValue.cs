using Newtonsoft.Json;

namespace SimpleValuation.Models
{
    /* https://financialmodelingprep.com/developer/docs#Company-Enterprise-Value */
    public class EnterpriseValue
    {
        [JsonProperty("numberOfShares")]
        public double NumberofShares { get; set; }
    }
}
