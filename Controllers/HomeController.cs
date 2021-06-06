using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleValuation.Models;

namespace SimpleValuation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient client = new HttpClient();

        private Dictionary<string, string> stocks;
        public static HashSet<string> watchlist = new HashSet<string>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            GetTickers().Wait();
        }

        [HttpGet]
        public IActionResult StockOverview(string id, double growthRate)
        {
            return View(new StockModel(id, growthRate));
        }

        [HttpPost]
        public RedirectToActionResult StockOverview(StockModel stock)
        {
            if (!isStockTickerValid(stock))
                return RedirectToAction("Index");

            watchlist.Add(stock.StockTicker);
            return RedirectToAction("StockOverview", new { id = stock.StockTicker.ToUpper(), stock.GrowthRate });
        }

        private bool isStockTickerValid(StockModel stock)
        {
            bool valid = stocks.ContainsKey(stock.StockTicker.ToUpper());

            if (valid) return true;

            KeyValuePair<string, string> searchResult = stocks.FirstOrDefault(s => s.Value.ToUpper().Contains(stock.StockTicker.ToUpper()));
            if (searchResult.Equals(new KeyValuePair<string, string>())) return false;

            stock.StockTicker = searchResult.Key;
            return true;
        }

        public IActionResult Index()
        {
            ViewData.Add("watchlist", getWatchlistString());
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        private async Task GetTickers()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var tickerTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/company/stock/list?apikey=" + Startup.FMF_APIKEY);
            string tickerData = await tickerTask;

            StockList symbolsList = JsonConvert.DeserializeObject<StockList>(tickerData);
            this.stocks = symbolsList.symbolsList.ToDictionary(s => s.symbol, s => s.name);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string getWatchlistString()
        {
            if (watchlist.Count < 1) return "{ \"proName\": \"SPX\"}";

            StringBuilder sb = new StringBuilder();

            watchlist.ToList().ForEach(ticker =>
            {
                sb.Append('{');
                sb.Append("\"description\": \"\",");
                sb.Append("\"proName\": \"");
                sb.Append(ticker);
                sb.Append("\" },");
            });

            sb.Length--;

            return sb.ToString();
        }
    }
}
