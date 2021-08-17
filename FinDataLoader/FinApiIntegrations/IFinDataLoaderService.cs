using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinApiIntegrations.Data;

namespace FinApiIntegrations
{
    public interface IFinDataLoaderService
    {
        Task<MarketSelection> LoadAsync(string range, string interval, string stock);
    }
}
