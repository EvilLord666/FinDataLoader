﻿using System;
using System.Collections.Generic;
using System.Text;
using FinDataLoader.Common.Data;
using FinDataLoader.Common.Services;
using FinDataLoader.Common.Tests.Checkers;
using Xunit;

namespace FinDataLoader.Common.Tests.Services
{
    public class TestSimpleMarketDataCompressor
    {
        [Fact]
        public void TestCompressDataWeekly()
        {
            MarketSelection data = new MarketSelection()
            {
                Timestamps = new List<DateTime>() 
                { 
                    new DateTime(2021, 8, 12), 
                    new DateTime(2021, 8, 13), 
                    new DateTime(2021, 8, 16) , 
                    new DateTime(2021, 8, 18) , 
                    new DateTime(2021, 8, 21) 
                },

                Open = new List<decimal>()
                {
                    99, 102, 105, 100, 90
                },

                Close = new List<decimal>()
                {
                    100, 101, 107, 110, 112
                },

                High = new List<decimal>()
                { 
                    120, 122, 125, 117, 129
                },

                Low = new List<decimal>()
                { 
                    94, 91, 85, 88, 90
                }
            };

            IList<CompressedMarketSelectionData> actualCompressedData = _compressor.Compress(data, CompessionOption.Week);
            IList<CompressedMarketSelectionData> expectedCompressedData = new List<CompressedMarketSelectionData>()
            {
                new CompressedMarketSelectionData(new DateTime(2021, 8, 12), new DateTime(2021, 8, 13), 99, 101, 122, 91),
                new CompressedMarketSelectionData(new DateTime(2021, 8, 16), new DateTime(2021, 8, 21), 105, 112, 129, 85)
            };

            CompressedMarketSelectionDataChecker.Check(expectedCompressedData, actualCompressedData);
        }

        [Fact]
        public void TestCompressDataMontly()
        {
            MarketSelection data = new MarketSelection()
            {
                Timestamps = new List<DateTime>()
                {
                    new DateTime(2021, 6, 10),
                    new DateTime(2021, 6, 13),
                    new DateTime(2021, 7, 22),
                    new DateTime(2021, 7, 29),
                    new DateTime(2021, 8, 16) ,
                    new DateTime(2021, 8, 18) ,
                    new DateTime(2021, 8, 21)
                },

                Open = new List<decimal>()
                {
                    80, 84, 99, 102, 105, 100, 90
                },

                Close = new List<decimal>()
                {
                    85, 88, 100, 101, 107, 110, 112
                },

                High = new List<decimal>()
                {
                    104, 103, 120, 122, 125, 117, 129
                },

                Low = new List<decimal>()
                {
                    70, 72, 94, 91, 85, 88, 90
                }
            };

            IList<CompressedMarketSelectionData> actualCompressedData = _compressor.Compress(data, CompessionOption.Month);
            IList<CompressedMarketSelectionData> expectedCompressedData = new List<CompressedMarketSelectionData>()
            {
                new CompressedMarketSelectionData(new DateTime(2021, 6, 10), new DateTime(2021, 6, 13), 80, 88, 104, 70),
                new CompressedMarketSelectionData(new DateTime(2021, 7, 22), new DateTime(2021, 7, 29), 99, 101, 122, 91),
                new CompressedMarketSelectionData(new DateTime(2021, 8, 16), new DateTime(2021, 8, 21), 105, 112, 129, 85)
            };

            CompressedMarketSelectionDataChecker.Check(expectedCompressedData, actualCompressedData);
        }

        private IMarketSelectionDataCompressor _compressor = new SimpleMarketDataCompressor();
    }
}
