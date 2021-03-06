using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinDataLoader.Common.Data;

namespace FinDataLoader.Export
{
    public interface IMarketSelectionDataExportService
    {
        public Task<bool> ExportAsync(string fileName, MarketSelection data, IDictionary<string, string> exportParams);
        public Task<bool> ExportAsync(string fileName, IList<CompressedMarketSelectionData> data, IDictionary<string, string> exportParams);
    }
}
