using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinDataLoader.Common.Data;

namespace FinDataLoader.Export
{
    interface IMarketSelectionDataExportService
    {
        public Task<bool> ExportAsync(MarketSelection data, IDictionary<string, string> exportParams);
    }
}
