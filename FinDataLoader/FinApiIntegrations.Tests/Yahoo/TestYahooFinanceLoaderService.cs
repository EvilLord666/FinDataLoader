using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using FinDataLoader.Integrations;
using FinDataLoader.Integrations.Yahoo;
using FinDataLoader.Common.Data;

namespace FinDataLoader.Integrations.Tests.Yahoo
{
    public class TestYahooFinanceLoaderService
    {
        [Theory]
        [InlineData("2y", "1d", "quote")]
        public void TestLoadAsyncNonEmptyData(string range, string interval, string indicators)
        {
            Task<MarketSelection> loadTask = _loader.LoadAsync(range, interval, indicators);
            loadTask.Wait();
            MarketSelection selection = loadTask.Result;
            Assert.NotNull(selection);
            // todo: umv: because we don't know how to properly check: just see that they are not empty
            Assert.True(selection.Timestamps.Count > 0);
            Assert.True(selection.Open.Count > 0);
            Assert.True(selection.Close.Count > 0);
            Assert.True(selection.High.Count > 0);
            Assert.True(selection.Low.Count > 0);
            Assert.True(selection.Volume.Count > 0);
        }

        private IFinDataLoaderService _loader = new YahooFinanceLoaderService(new LoggerFactory());
    }
}
