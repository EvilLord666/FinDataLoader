using FinDataLoader.Common.Data;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace FinDataLoader.Common.Services
{
    internal class Range
    { 
        public int Begin { get; set; }
        public int End { get; set; }
    }

    public class SimpleMarketDataCompressor : IMarketSelectionDataCompressor
    {
        public MarketSelection Compress(MarketSelection data, CompessionOption option)
        {
            throw new NotImplementedException();
            // analyze data split by indexes
        }

        private IList<Range> GetIndexRanges(MarketSelection data, CompessionOption option)
        {
            IList<Range> ranges = new List<Range>();

            return ranges;
        }

        private bool CheckDatesAreFromSameWeek(DateTime initial, DateTime current)
        {
            Calendar calendar = _cultureInfo.Calendar;
            CalendarWeekRule weekRule = _cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = _cultureInfo.DateTimeFormat.FirstDayOfWeek;
            int initialDateWeekNumber = calendar.GetWeekOfYear(initial, weekRule, firstDayOfWeek);
            int currentDateWeekNumber = calendar.GetWeekOfYear(initial, weekRule, firstDayOfWeek);
            return initialDateWeekNumber == currentDateWeekNumber;
        }

        private bool CheckDatesAreFromSameMonth(DateTime initial, DateTime current)
        {
            return initial.Month == current.Month && initial.Year == current.Year;
        }

        private bool CheckDatesAreFromSameYear(DateTime initial, DateTime current)
        {
            return initial.Year == current.Year;
        }

        private CultureInfo _cultureInfo = new CultureInfo("ru-RU");
    }
}
