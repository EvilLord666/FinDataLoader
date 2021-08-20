using System;
using System.Collections.Generic;
using System.Text;
using FinDataLoader.Common.Data;

namespace FinDataLoader.Common.Services
{
    public enum CompessionOption
    { 
        Week = 1,
        Month = 2,
        Year = 3
    }

    public interface IMarketSelectionDataCompressor
    {
        public IList<CompressedMarketSelectionData> Compress(MarketSelection data, CompessionOption option);
    }
}
