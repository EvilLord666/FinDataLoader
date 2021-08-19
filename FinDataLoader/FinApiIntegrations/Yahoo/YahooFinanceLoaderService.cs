using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FinDataLoader.Integrations;
using FinDataLoader.Common.Data;

namespace FinDataLoader.Integrations.Yahoo
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
                using (HttpClient httpClient = new HttpClient())
                {
                    string urlWithParams = String.Format(YahooFinanceStatUrlTemplate, range, interval, stock);
                    HttpResponseMessage response = await httpClient.GetAsync(urlWithParams);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // todo: umv: parse
                    JToken dataRoot = JObject.Parse(responseBody)["chart"]["result"][0];
                    IList<DateTime> timestamps = dataRoot["timestamp"].ToObject<List<long>>().Select(t => Convert(t)).ToList();
                    // todo: umv: build selection
                    IList<decimal> open = dataRoot["indicators"][stock][0]["open"].ToObject<List<decimal>>();
                    IList<decimal> close = dataRoot["indicators"][stock][0]["close"].ToObject<List<decimal>>();
                    IList<decimal> high = dataRoot["indicators"][stock][0]["high"].ToObject<List<decimal>>();
                    IList<decimal> low = dataRoot["indicators"][stock][0]["low"].ToObject<List<decimal>>();
                    IList<long> volume = dataRoot["indicators"][stock][0]["volume"].ToObject<List<long>>();
                    return new MarketSelection(timestamps, open, close, high, low, volume);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An error ocurred during Yahoo finance data load: {e.Message}");
                return null;
            }

            throw new NotImplementedException();
        }

        private DateTime Convert(long timeStamp)
        {
            DateTime initial = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = initial.AddSeconds(timeStamp).ToLocalTime();
            return date;
        }

        private const string YahooBaseUrl = "https://query1.finance.yahoo.com/v7/finance/chart/AAPL";
        private const string YahooFinanceStatUrlTemplate = YahooBaseUrl + "?range={0}&interval={1}&indicators={2}&includeTimestamps=true";

        private ILogger<YahooFinanceLoaderService> _logger;
    }
}
