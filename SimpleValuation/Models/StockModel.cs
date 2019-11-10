using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleValuation.Models
{
    public class StockModel
    {

        public StockModel()
        {

        }

        public StockModel(string ticker)
        {
            StockTicker = ticker;
        }

        public string StockTicker { get; set; }

        public bool IsMicrosoft
        {
            get { return StockTicker.ToUpper().Equals("MSFT"); }
            set { }
        }

    }
    
}
