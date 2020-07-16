using NStandard;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Dawnx.NPOI.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var book = new ExcelBook(ExcelVersion.Excel2007);

            var ����style2 = book.CStyle(x => x.CellFormat("0.00").CellColor(RGBColor.Blue).SetFont("����", 20).FullBorder());
            var ����style1 = book.CStyle(x => x.CellFormat("0.00").CellColor(RGBColor.Blue).SetFont("����", 20).FullBorder());
            var ����style1 = book.CStyle(x => x.CellFormat("0.00").CellColor(RGBColor.Red).SetFont("����", 27, RGBColor.BlueGrey).FullBorder());

            var sampleSheet = book.CreateSheet("Sample");
            sampleSheet.SetWidth("A", 8.5);
            sampleSheet.SetWidth("B", 3);
            sampleSheet.SetWidth("C", 30);

            sampleSheet.SetCursor("C2");

            sampleSheet.PrintLine("Suplier", "Product", "Quantity", "Release Date", "Describe");
            sampleSheet.Print(new object[,]
            {
                { "Company 101", "Ag", 1, DateTime.Parse("2017-1-1"), "[??]" },
                { "Company 101", "Ag", 1, DateTime.Parse("2017-1-1"), "[??]" },
                { "Company 101", "Ag", 2, DateTime.Parse("2017-1-2"), "[??]" },
                { "Company 101", "Ag", 2, DateTime.Parse("2017-1-2"), "[??]" },
                { "Company 102", "Ag", 3, DateTime.Parse("2017-1-3"), "[??]" },
                { "Company 102", "Cu", 3, DateTime.Parse("2017-1-3"), "[??]" },
                { "Company 102", "Cu", 4, DateTime.Parse("2017-1-4"), "[??]" },
            }).Then(range =>
            {
                range.SetCStyle(����style1);
                var cstyle = range.ToArray()[0].GetCStyle();
                var applier = cstyle.GetApplier();

                Assert.Equal("0.00", applier.DataFormat);

                foreach (var row in range.GetRows())
                {
                    if (row[(0, 1)].GetValue().ToString() == "Cu")
                        row.Column(1).SetCStyle(����style1);
                }
                range.SmartMerge(new[] { 0, 1, 2, 3 });
            });

            sampleSheet["E4"].SetValue(123);

            sampleSheet.SetCursor("A12");
            sampleSheet.PrintLine(new[] { "[[1]]A", "[[1]]A", "[[2]]A", "[[2]]B", "[[2]]B" });
            sampleSheet["A12", "E12"].SmartColMerge();

            //var dawnxSheet = book.CloneSheet( sampleSheet;
            //var cell = sheet["C2"];

            //var sheet = book.CloneSheet("Products");
            var dawnxSheet = sampleSheet.Clone("dawnx");

            var memory = new MemoryStream();
            book.Write(memory);



            //sheet["A2", "C8"].SmartMerge(new[] { 0, 2, 1 });
            book.SaveAs(@"result1.xlsx");

            //var book = new XSSFWorkbook(@"C:\Users\19558\Desktop\e1.xlsx");
            //var book2 = new HSSFWorkbook();
            //var sheet = book.GetSheetAt(0).Dawn();
            //var table = sheet.FetchTable("A3", true,
            //    new[] { typeof(int), typeof(int), typeof(string) });

            //sheet["A1"].Cell.SetValue(123);
        }

        [Fact]
        public void FetchTest()
        {
            var book = new ExcelBook("NPOI/test.xlsx");
            var models = book[0].Fetch<Model>("A3", m => new { m.Supplier, m.Product, m.Number, m.ReleaseDate });

            Assert.Equal("Company 101", models[0].Supplier);
            Assert.Equal("Ag", models[0].Product);
            Assert.Equal(new DateTime(2018, 1, 1), models[0].ReleaseDate);
        }

        [Fact]
        public void VersionTest()
        {
            Assert.Equal(ExcelVersion.Excel2007, ExcelBook.GetVersion("no_file.xlsx"));
        }

        public class Model
        {
            public string Supplier { get; set; }
            public string Product { get; set; }
            public int? Number { get; set; }
            public DateTime ReleaseDate { get; set; }
        }

    }
}
