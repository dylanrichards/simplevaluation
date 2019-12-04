using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleValuation.Models
{
    public class News
    {
        public string symbol { get; set; }

        public NewsData[] data;

        public class NewsData
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("source_name")]
            public string Source { get; set; }

            [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("news_url")]
            public string NewsUrl { get; set; }

        }
    }
}
