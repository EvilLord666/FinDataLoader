﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinDataLoader.Dto;

namespace FinDataLoader.WebApi.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketSelectionController : ControllerBase
    {
        // https://localhost:44368/api/market/yahoo/compressed
        [HttpGet("/api/market/yahoo/compressed")]
        public async Task<IList<CompressedMarketSelectionDto>> getYahooCompressedMarketSelection()
        {
            return null;
        }
    }
}
