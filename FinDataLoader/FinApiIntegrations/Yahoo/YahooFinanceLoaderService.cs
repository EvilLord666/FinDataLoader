using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FinApiIntegrations;
using FinApiIntegrations.Data;

namespace FinApiIntegrations.Yahoo
{
    public class YahooFinanceLoaderService : IFinDataLoaderService
    {
        public YahooFinanceLoaderService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<YahooFinanceLoaderService>();
        }

        public async Task<MarketSelection> LoadAsync(string range, string interval, string stock)
        {
            try
            {
                MarketSelection selection = new MarketSelection();

                using (HttpClient httpClient = new HttpClient())
                {
                    string urlWithParams = String.Format(YahooFinanceStatUrlTemplate, range, interval, stock);
                    HttpResponseMessage response = await httpClient.GetAsync(urlWithParams);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // todo: umv: parse
                    JToken dataRoot = JObject.Parse(responseBody)["chart"]["result"][0];
                    var timestamps = dataRoot["timestamp"].Value<object[]>();
                    // todo: umv: build selection
                    int a = 1;
                }

                return selection;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error ocurred during Yahoo finance data load: {e.Message}");
                return null;
            }

            throw new NotImplementedException();
        }

        private const string YahooBaseUrl = "https://query1.finance.yahoo.com/v7/finance/chart/AAPL";
        private const string YahooFinanceStatUrlTemplate = YahooBaseUrl + "?range={0}&interval={1}&indicators={2}&includeTimestamps=true";

        private ILogger<YahooFinanceLoaderService> _logger;
    }
}
