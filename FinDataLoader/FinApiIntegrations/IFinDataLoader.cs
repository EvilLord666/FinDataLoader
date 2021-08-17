using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinApiIntegrations.Data;

namespace FinApiIntegrations
{
    public interface IFinDataLoader
    {
        Task<MarketSelection> LoadAsync(string range, string interval, string stock);
    }
}
