using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SimpleValuation.Models
{
    /* https://financialmodelingprep.com/developer/docs#Company-Profile */
    public class CompanyProfile
    {
        public string symbol { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [JsonProperty("price")]
        public double CurrentPrice { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("website")]
        public string CompanyWebsite { get; set; }

        [JsonProperty("image")]
        public string CompanyLogo { get; set; }
    }
}
