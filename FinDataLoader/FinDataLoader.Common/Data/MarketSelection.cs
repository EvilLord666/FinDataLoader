using System;
using System.Collections.Generic;
using System.Text;

namespace FinDataLoader.Common.Data
{
    public class MarketSelection
    {
        public MarketSelection() 
        {
            Timestamps = new List<DateTime>();
            Open = new List<decimal>();
            Close = new List<decimal>();
            High = new List<decimal>();
            Low = new List<decimal>();
            Volume = new List<long>();
        }

        public MarketSelection(IList<DateTime> timestamps, IList<decimal> open, IList<decimal> close,
                               IList<decimal> high, IList<decimal> low, IList<long> volume)
        {
            Timestamps = timestamps;
            Open = open;
            Close = close;
            High = high;
            Low = low;
            Volume = volume;
        }

        public IList<DateTime> Timestamps { get; set; }

        public IList<decimal> Open { get; set; }

        public IList<decimal> Close { get; set; }

        public IList<decimal> High { get; set; }

        public IList<decimal> Low { get; set; }

        public IList<long> Volume { get; set; }
    }
}
