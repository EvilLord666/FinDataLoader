using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinDataLoader.Common.Data;

namespace FinDataLoader.Integrations
{
    public interface IFinDataLoaderService
    {
        Task<MarketSelection> LoadAsync(string range, string interval, string stock);
    }
}
