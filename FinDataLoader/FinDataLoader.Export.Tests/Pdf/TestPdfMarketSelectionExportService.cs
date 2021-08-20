using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinDataLoader.Common.Data;
using FinDataLoader.Export;
using FinDataLoader.Export.Pdf;
using Microsoft.Extensions.Logging;
using Xunit;

namespace FinDataLoader.Export.Tests.Pdf
{
    public class TestPdfMarketSelectionExportService
    {
        [Fact]
        public void TestExportAsyncOnePageData()
        {
            string pdfFileName = @".\onePageData.pdf";
            if (File.Exists(pdfFileName))
                File.Delete(pdfFileName);

            MarketSelection data = new MarketSelection();
            data.Timestamps.Add(new DateTime());
            data.Open.Add(100);
            data.Close.Add(90);
            data.High.Add(122);
            data.Low.Add(78);
            data.Volume.Add(602);
            Task<bool> exportTask = _exportService.ExportAsync(pdfFileName, data, null);
            exportTask.Wait();
            bool result = exportTask.Result;
            Assert.True(result);

            if (File.Exists(pdfFileName))
                File.Delete(pdfFileName);
        }

        private IMarketSelectionDataExportService _exportService = new PdfMarketSelectionExportService(new LoggerFactory());
    }
}
