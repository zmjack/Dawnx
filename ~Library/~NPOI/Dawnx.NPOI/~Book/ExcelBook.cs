using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dawnx.NPOI
{
    public partial class ExcelBook
    {
        public IWorkbook MapedWorkbook { get; private set; }
        public string FilePath { get; private set; }
        public ExcelVersion Version { get; private set; }

        public ExcelBook(ExcelVersion version)
        {
            Version = version;
            MapedWorkbook = Create(version);
        }
        public ExcelBook(string path) : this(path, GetVersion(path)) { }

        public ExcelBook(string path, ExcelVersion version)
        {
            Version = version;
            FilePath = path;
            if (!File.Exists(path))
                throw new FileNotFoundException();

            using (var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                MapedWorkbook = Open(file, version);
        }

        public ExcelBook(Stream stream, ExcelVersion version)
        {
            Version = version;
            MapedWorkbook = Open(stream, version);
        }

        public short GetDataFormat(string format) => MapedWorkbook.CreateDataFormat().GetFormat(format);

        public IEnumerable<ICellStyle> CellStyles => Range.Create(NumCellStyles).Select(i => GetCellStyleAt((short)i));
        public IEnumerable<BookCellStyle> BookCellStyles => CellStyles.Select(x => new BookCellStyle(this, x));
        public BookCellStyle BookCellStyleAt(short index) => new BookCellStyle(this, GetCellStyleAt(index));
        public BookCellStyle BookCellStyle(Action<BookCellStyleApplier> init) => BookCellStyle(BookCellStyleApplier.Create(init));
        public BookCellStyle BookCellStyle(BookCellStyleApplier applier)
        {
            var find = BookCellStyles.FirstOrDefault(x => x.InterfaceValuesEqual(applier));
            if (find is null)
                return new BookCellStyle(this).Self(_ => applier.Apply(_));
            else return find;
        }

        public IEnumerable<IFont> Fonts => Range.Create(NumberOfFonts).Select(i => GetFontAt((short)i));
        public IEnumerable<BookFont> BookFonts => Fonts.Select(x => new BookFont(this, x));
        public BookFont BookFontAt(short index) => new BookFont(this, GetFontAt(index));
        public BookFont BookFont(Action<BookFontApplier> init) => BookFont(BookFontApplier.Create(init));
        internal BookFont BookFont(BookFontApplier applier)
        {
            var find = BookFonts.FirstOrDefault(x => x.InterfaceValuesEqual(applier));
            if (find is null)
                return new BookFont(this).Self(_ => applier.Apply(_));
            else return find;
        }

        public ExcelSheet this[int index] => GetSheetAt(index);
        public ExcelSheet this[string name] => GetSheet(name);

        private IWorkbook Open(Stream stream, ExcelVersion version)
        {
            switch (version)
            {
                case ExcelVersion.Excel2003:
                    return new HSSFWorkbook(stream);
                case ExcelVersion.Excel2007:
                    return new XSSFWorkbook(stream);
                default: throw new NotSupportedException();
            }
        }

        private IWorkbook Create(ExcelVersion version)
        {
            switch (version)
            {
                case ExcelVersion.Excel2003:
                    return new HSSFWorkbook();
                case ExcelVersion.Excel2007:
                    return new XSSFWorkbook();
                default: throw new NotSupportedException();
            }
        }

        public static ExcelVersion GetVersion(string path)
        {
            switch (Path.GetExtension(path))
            {
                case ".xls": return ExcelVersion.Excel2003;
                case ".xlsx": return ExcelVersion.Excel2007;
                default: throw new NotSupportedException();
            }
        }

        public void Save()
        {
            if (FilePath is null)
                throw new FileNotFoundException("Has no specified file path.");

            using (var file = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                Write(file);
        }

        public void SaveAs(string path)
        {
            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
                Write(file);
        }

    }
}
