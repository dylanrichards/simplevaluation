using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
}
