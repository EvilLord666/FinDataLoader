using System;
using System.Collections.Generic;
using System.Text;
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
            
        }

        private IMarketSelectionDataExportService _exportService = new PdfMarketSelectionExportService(new LoggerFactory());
    }
}
