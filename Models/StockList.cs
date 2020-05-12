using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleValuation.Models
{
    public class StockList
    {
        public SymbolsList[] symbolsList;

        public class SymbolsList
        {
            [JsonProperty("symbol")]
            public string symbol { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }
        }        
    }
}
