using Newtonsoft.Json;

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
