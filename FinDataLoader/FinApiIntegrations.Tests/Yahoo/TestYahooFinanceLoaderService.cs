using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using FinApiIntegrations;
using FinApiIntegrations.Data;
using FinApiIntegrations.Yahoo;

namespace FinApiIntegrations.Tests.Yahoo
{
    public class TestYahooFinanceLoaderService
    {
        [Theory]
        [InlineData("2y", "1d", "quote")]
        public void TestLoadAsync(string range, string interval, string indicators)
        {
            Task<MarketSelection> loadTask = _loader.LoadAsync(range, interval, indicators);
            loadTask.Wait();
            MarketSelection selection = loadTask.Result;

            Assert.NotNull(selection);
        }

        private IFinDataLoaderService _loader = new YahooFinanceLoaderService(new LoggerFactory());
    }
}
