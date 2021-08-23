using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinDataLoader.Dto;
using FinDataLoader.Common.Services;
using FinDataLoader.WebApi.Managers;

namespace FinDataLoader.WebApi.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketSelectionController : ControllerBase
    {
        public MarketSelectionController(ILoggerFactory loggerFactory)
        {
            // todo: umv: create scoped via DI
            _manager = new MarketSelectionManager(loggerFactory);
        }

        // https://localhost:44368/api/market/yahoo/compressed/?range=2y&interval=1d&indicators=quote&compress=week
        [HttpGet("/api/market/yahoo/compressed")]
        public async Task<IList<CompressedMarketSelectionDto>> GetYahooCompressedMarketSelection([FromQuery] string range, [FromQuery] string interval, 
                                                                                                 [FromQuery] string indicators, [FromQuery] string compress)
        {
            // todo: umv: i suppose that indicators should be array i.e. ?indicators=ind1&indicators=ind2&...
            CompessionOption? option = _compressOptions[compress.ToLower()];
            if (option == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
            IList<CompressedMarketSelectionDto> result = await _manager.GetYahooCompressedMarketSelection(range, interval, indicators, option.Value);
            if (result == null)
            {
                // todo: umv: think about how to handle 
                // assume that we are having bad params, therefore we return 400
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }

            return result;
        }

        // https://localhost:44368/api/market/yahoo/compressed/pdf/?range=2y&interval=1d&indicators=quote&compress=week
        [HttpGet("/api/market/yahoo/compressed/pdf")]
        public async Task<FileContentResult> GetYahooCompressedMarketSelectionInPdf([FromQuery] string range, [FromQuery] string interval,
                                                                                    [FromQuery] string indicators, [FromQuery] string compress)
        {
            CompessionOption? option = _compressOptions[compress.ToLower()];
            if (option == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
            string fileName = $"Yahoo_{range}_{interval}_{indicators}_{compress}_{Guid.NewGuid()}.pdf";
            bool result = await _manager.ExportYahooCompressedMarketSelection(fileName, range, interval, indicators, option.Value);
            if (!result)
            {
                // todo: umv: think about how to handle 
                // assume that we are having bad params, therefore we return 400
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }
            using (var net = new System.Net.WebClient())
            {
                byte[] data = await net.DownloadDataTaskAsync(fileName);
                return new FileContentResult(data, "application/pdf")
                {
                    FileDownloadName = fileName
                };
            }
        }

        private readonly MarketSelectionManager _manager;
        private readonly IDictionary<string, CompessionOption> _compressOptions = new Dictionary<string, CompessionOption>()
        {
            { "week", CompessionOption.Week },
            { "month", CompessionOption.Month },
            { "year", CompessionOption.Year }
        };
    }
}
