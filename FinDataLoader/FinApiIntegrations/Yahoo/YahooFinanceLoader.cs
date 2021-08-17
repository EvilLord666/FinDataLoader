using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinApiIntegrations;
using FinApiIntegrations.Data;

namespace FinApiIntegrations.Yahoo
{
    public class YahooFinanceLoader : IFinDataLoader
    {
        public async Task<MarketSelection> LoadAsync(string range, string interval, string stock)
        {
            throw new NotImplementedException();
        }
    }
}
