using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinDataLoader.Common.Data;
using FinDataLoader.Dto;

namespace FinDataLoader.WebApi.Factories
{
    public static class CompressedMarketSelectionDataFactory
    {
        public static CompressedMarketSelectionDto Create(CompressedMarketSelectionData data)
        {
            return new CompressedMarketSelectionDto(data.Begin, data.End, data.Open, data.Close, data.High, data.Low);
        }
    }
}
