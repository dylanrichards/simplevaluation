using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleValuation.Models
{
    public class EnterpriseValue
    {

        public EnterpriseValues[] enterpriseValues;
        
        public class EnterpriseValues
        {
            [JsonProperty("Number of Shares")]
            public double NumberofShares { get; set; }
        }

    }
}
