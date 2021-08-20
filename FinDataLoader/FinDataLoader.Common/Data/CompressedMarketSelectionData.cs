using System;
using System.Collections.Generic;
using System.Text;

namespace FinDataLoader.Common.Data
{
    public class CompressedMarketSelectionData
    {
        public CompressedMarketSelectionData(DateTime begin, DateTime end, decimal open, decimal close,
                                             decimal high, decimal low)
        {
            Begin = begin;
            End = end;
            Open = open;
            Close = close;
            High = high;
            Low = low;
        }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
    }
}
