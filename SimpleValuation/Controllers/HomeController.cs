using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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

                
            return RedirectToAction("StockOverview", new { id = stock.StockTicker.ToUpper(),  stock.GrowthRate});
        }

        private bool isStockTickerValid(StockModel stock)
        {
            bool valid = stocks.ContainsKey(stock.StockTicker.ToUpper());

            if (valid) return true;

            KeyValuePair<string, string> searchResult = stocks.FirstOrDefault(s => s.Value.Contains(stock.StockTicker));
            if (searchResult.Equals(new KeyValuePair<string, string>())) return false;

            stock.StockTicker = searchResult.Key;
            return true;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private async Task GetTickers()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var tickerTask = client.GetStringAsync("https://financialmodelingprep.com/api/v3/company/stock/list");
            string tickerData = await tickerTask;

            StockList symbolsList = JsonConvert.DeserializeObject<StockList>(tickerData);
            this.stocks = symbolsList.symbolsList.ToDictionary(s => s.symbol, s => s.name);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
