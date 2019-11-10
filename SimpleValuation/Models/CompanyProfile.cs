using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleValuation.Models
{
    public class CompanyProfile
    {
        public string symbol { get; set; }

        public Profile profile { get; set; }

        public class Profile
        {
            [JsonProperty("price")]
            public double CurrentPrice { get; set; }
            [JsonProperty("companyName")]
            public string CompanyName { get; set; }
            [JsonProperty("website")]
            public string CompanyWebsite { get; set; }
            [JsonProperty("image")]
            public string CompanyLogo { get; set; }
        }

    }
}
