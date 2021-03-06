using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FinDataLoader.Common.Services;
using FinDataLoader.Common.Data;
using FinDataLoader.Dto;
using FinDataLoader.Integrations;
using FinDataLoader.Integrations.Yahoo;
using FinDataLoader.WebApi.Factories;
using FinDataLoader.Export;
using FinDataLoader.Export.Pdf;

namespace FinDataLoader.WebApi.Managers
{
    public class MarketSelectionManager
    {
        public MarketSelectionManager(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MarketSelectionManager>();
            // todo: umv: instantiate services via DI
            _findataLoaderService = new YahooFinanceLoaderService(loggerFactory);
            _compressorService = new SimpleMarketDataCompressor(loggerFactory);
            _exportService = new PdfMarketSelectionExportService(loggerFactory);
        }

        public async Task<IList<CompressedMarketSelectionDto>> GetYahooCompressedMarketSelection(string range, string interval, string indicators, 
                                                                                                 CompessionOption compressOption)
        {
            try
            {
                MarketSelection selection = await _findataLoaderService.LoadAsync(range, interval, indicators);
                IList<CompressedMarketSelectionData> compressedData = _compressorService.Compress(selection, compressOption);
                return compressedData.Select(d => CompressedMarketSelectionDataFactory.Create(d)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during getting compressed market selection data from Yahoo: {e.Message}");
                return null;
            }
        }

        public async Task<bool> ExportYahooCompressedMarketSelection(string fileName, string range, string interval, string indicators,
                                                                       CompessionOption compressOption)
        {
            try
            {
                MarketSelection selection = await _findataLoaderService.LoadAsync(range, interval, indicators);
                IList<CompressedMarketSelectionData> compressedData = _compressorService.Compress(selection, compressOption);
                return await _exportService.ExportAsync(fileName, compressedData, null);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during getting compressed market selection data from Yahoo: {e.Message}");
                return false;
            }
        }

        private ILogger<MarketSelectionManager> _logger;
        private IFinDataLoaderService _findataLoaderService;
        private IMarketSelectionDataCompressor _compressorService;
        private IMarketSelectionDataExportService _exportService;
    }
}
