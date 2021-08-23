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
        public MarketSelectionController(LoggerFactory loggerFactory)
        {
            // todo: umv: create scoped via DI
            _manager = new MarketSelectionManager(loggerFactory);
        }

        // https://localhost:44368/api/market/yahoo/compressed
        [HttpGet("/api/market/yahoo/compressed")]
        public async Task<IList<CompressedMarketSelectionDto>> GetYahooCompressedMarketSelection()
        {
            IList<CompressedMarketSelectionDto> result = await _manager.GetYahooCompressedMarketSelection("2y", "1d", "quotes", CompessionOption.Week);
            if (result == null)
            {
                // todo: umv: think about how to handle 
                // assume that we are having bad params, therefore we return 400
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            return result;
        }

        private MarketSelectionManager _manager;
    }
}
