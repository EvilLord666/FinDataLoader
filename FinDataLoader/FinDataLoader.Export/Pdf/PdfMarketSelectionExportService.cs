using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using PdfSharpCore.Fonts;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using FinDataLoader.Common.Data;
using FinDataLoader.Export;

namespace FinDataLoader.Export.Pdf
{
    public class PdfMarketSelectionExportService : IMarketSelectionDataExportService
    {
        public async Task<bool> ExportAsync(string fileName, MarketSelection data, IDictionary<string, string> exportParams)
        {
            try
            {
                PdfDocument document = new PdfDocument(fileName);
                //List<ItemsToDisplay> pdfRows = new List<ItemsToDisplay>();
                int rowsPerPage = GetRowsPerPage();
                PdfPage currentPage = null;
                XGraphics gfx = null;
                bool done = false;
                int pageNumber = 0;

                while (!done)
                {
                    IList<DateTime> timestampsPageSelection = data.Timestamps.Skip(pageNumber * rowsPerPage).Take(rowsPerPage).ToList();
                    if (timestampsPageSelection.Count == 0)
                    {
                        done = true;
                        gfx?.Dispose();
                    }
                    else
                    {
                        // we know that all data properties are collections of a same length
                        currentPage = document.AddPage();
                        gfx = XGraphics.FromPdfPage(currentPage);
                        IList<decimal> openPageSelection = data.Open.Skip(pageNumber * rowsPerPage).Take(rowsPerPage).ToList();
                        IList<decimal> closePageSelection = data.Close.Skip(pageNumber * rowsPerPage).Take(rowsPerPage).ToList();
                        IList<decimal> highPageSelection = data.High.Skip(pageNumber * rowsPerPage).Take(rowsPerPage).ToList();
                        IList<decimal> lowPageSelection = data.Low.Skip(pageNumber * rowsPerPage).Take(rowsPerPage).ToList();
                        IList<long> volumePageSelection = data.Volume.Skip(pageNumber * rowsPerPage).Take(rowsPerPage).ToList();

                        bool result = await ExportPagePortion(gfx, timestampsPageSelection, openPageSelection,
                                                              closePageSelection, highPageSelection, lowPageSelection, volumePageSelection);

                        if (!result)
                        {
                            return false;
                        }
                        else
                        {
                            gfx?.Dispose();
                            pageNumber++;
                        }
                    }
                }


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task<bool> ExportPagePortion(XGraphics gfx, IList<DateTime> timeStamps, IList<decimal> open, 
                                                   IList<decimal> close, IList<decimal> high, IList<decimal> low, 
                                                   IList<long> volumes)
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // todo: umv: stub, but should return number of rows depends on font, and other params
        private int GetRowsPerPage()
        {
            return 50;
        }
    }
}
