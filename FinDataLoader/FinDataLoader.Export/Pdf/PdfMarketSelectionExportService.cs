using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinDataLoader.Common.Data;
using FinDataLoader.Export;

namespace FinDataLoader.Export.Pdf
{
    public class PdfMarketSelectionExportService : IMarketSelectionDataExportService
    {
        public Task<bool> ExportAsync(MarketSelection data, IDictionary<string, string> exportParams)
        {
            throw new NotImplementedException();
        }
    }
}
