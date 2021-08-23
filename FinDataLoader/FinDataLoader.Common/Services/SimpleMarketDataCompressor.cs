using FinDataLoader.Common.Data;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

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
        // todo: umv: add logger here via LoggerFactory
        public SimpleMarketDataCompressor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SimpleMarketDataCompressor>();

            _comparators[CompessionOption.Week] = CheckDatesAreFromSameWeek;
            _comparators[CompessionOption.Month] = CheckDatesAreFromSameMonth;
            _comparators[CompessionOption.Year] = CheckDatesAreFromSameYear;
        }

        public IList<CompressedMarketSelectionData> Compress(MarketSelection data, CompessionOption option)
        {
            try
            {
                IList<CompressedMarketSelectionData> compressedSelection = new List<CompressedMarketSelectionData>();
                IList<Range> timeRanges = GetIndexRanges(data, option);

                foreach (Range range in timeRanges)
                {
                    //  1. Open - for initial index
                    decimal? openValue = null;
                    if (range.Begin < data.Open.Count)
                        openValue = data.Open[range.Begin];
                    //  2. Close - for last index
                    decimal? closeValue = null;
                    if (range.End < data.Open.Count)
                        closeValue = data.Close[range.End];
                    //  3. Hign - max value
                    IList<decimal> highValues = data.High.ToList().GetRange(range.Begin, range.End - range.Begin + 1);
                    decimal? highValue = null;
                    if (highValues.Any())
                        highValue = highValues.Max();
                    //  4. Low - min value
                    IList<decimal> lowValues = data.Low.ToList().GetRange(range.Begin, range.End - range.Begin + 1);
                    decimal? lowValue = null;
                    if (lowValues.Any())
                        lowValue = lowValues.Min();
                    compressedSelection.Add(new CompressedMarketSelectionData(data.Timestamps[range.Begin], data.Timestamps[range.End],
                                                                              openValue, closeValue, highValue, lowValue));
                }

                return compressedSelection;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during market data compression: {e.Message}");
                return null;
            }
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
                        ranges.Add(new Range(beginIndex.Value, endIndex ?? beginIndex.Value));
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
            int lastYearWeekNumber = calendar.GetWeekOfYear(new DateTime(initial.Year, 12, 31), weekRule, firstDayOfWeek);
            int currentDateWeekNumber = calendar.GetWeekOfYear(current, weekRule, firstDayOfWeek);
            if (initialDateWeekNumber == lastYearWeekNumber && currentDateWeekNumber == 1)
            {
                return true;
            }
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
        private ILogger<SimpleMarketDataCompressor> _logger;
    }
}
