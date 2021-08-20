using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using FinDataLoader.Common.Data;

namespace FinDataLoader.Common.Tests.Checkers
{
    internal static class CompressedMarketSelectionDataChecker
    {

        public static void Check(IList<CompressedMarketSelectionData> expected, IList<CompressedMarketSelectionData> actual)
        {
            Assert.Equal(expected.Count, actual.Count);
            foreach (CompressedMarketSelectionData e in expected)
            {
                CompressedMarketSelectionData a = actual.FirstOrDefault(item => item.Begin == e.Begin && item.End == e.End);
                Assert.NotNull(a);
                Check(e, a);
            }
        }

        public static void Check(CompressedMarketSelectionData expected, CompressedMarketSelectionData actual)
        {
            Assert.Equal(expected.Begin, actual.Begin);
            Assert.Equal(expected.End, actual.End);

            Assert.Equal(expected.Open, actual.Open);
            Assert.Equal(expected.Close, actual.Close);
            Assert.Equal(expected.High, actual.High);
            Assert.Equal(expected.Low, actual.Low);
        }
    }
}
