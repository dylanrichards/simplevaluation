using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleValuation.Models
{
    public class DiscountedCashFlow
    {

        [JsonProperty("DCF")]
        public double DCF { get; set; }

    }
}
