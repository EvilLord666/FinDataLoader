using FinDataLoader.Common.Data;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace FinDataLoader.Common.Services
{
    internal class Range
    {
        public Range(int begin, int end)
        {
            Begin = begin;
            End = end;
        }

        public int Begin { get; set; }
        public int End { get; set; }
    }

    public class SimpleMarketDataCompressor : IMarketSelectionDataCompressor
    {
        public SimpleMarketDataCompressor()
        {
            _comparators[CompessionOption.Week] = CheckDatesAreFromSameWeek;
            _comparators[CompessionOption.Month] = CheckDatesAreFromSameMonth;
            _comparators[CompessionOption.Year] = CheckDatesAreFromSameYear;
        }

        public MarketSelection Compress(MarketSelection data, CompessionOption option)
        {
            throw new NotImplementedException();
            // analyze data split by indexes
        }

        private IList<Range> GetIndexRanges(MarketSelection data, CompessionOption option)
        {
            IList<Range> ranges = new List<Range>();
            int? beginIndex = null;
            int? endIndex = null;

            Func<DateTime, DateTime, bool> checker = _comparators[option];

            for (int i = 0; i < data.Timestamps.Count; i++)
            {
                if (!beginIndex.HasValue)
                {
                    beginIndex = i;
                }

                if (i + 1 < data.Timestamps.Count)
                {
                    bool checkResult = checker(data.Timestamps[beginIndex.Value], data.Timestamps[i + 1]);
                    if (checkResult)
                    {
                        endIndex = i + 1;
                    }
                    else
                    {
                        ranges.Add(new Range(beginIndex.Value, endIndex.Value));
                        beginIndex = null;
                    }
                }
                else
                {
                    ranges.Add(new Range(beginIndex.Value, i));
                }
            }
            return ranges;
        }

        private bool CheckDatesAreFromSameWeek(DateTime initial, DateTime current)
        {
            Calendar calendar = _cultureInfo.Calendar;
            CalendarWeekRule weekRule = _cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = _cultureInfo.DateTimeFormat.FirstDayOfWeek;
            int initialDateWeekNumber = calendar.GetWeekOfYear(initial, weekRule, firstDayOfWeek);
            int currentDateWeekNumber = calendar.GetWeekOfYear(current, weekRule, firstDayOfWeek);
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

        private IDictionary<CompessionOption, Func<DateTime, DateTime, bool>> _comparators = new Dictionary<CompessionOption, Func<DateTime, DateTime, bool>>();
    }
}
