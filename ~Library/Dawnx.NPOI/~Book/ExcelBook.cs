using Dawnx.Definition;
using Dawnx.Ranges;
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

        public string MediaType
        {
            get
            {
                switch (Version)
                {
                    case ExcelVersion.Excel2003: return MimeType.Microsoft.EXCEL_2003;
                    case ExcelVersion.Excel2007: return MimeType.Microsoft.EXCEL_2007;
                    default: return string.Empty;
                }
            }
        }

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

        public ExcelBook(byte[] bytes, ExcelVersion version)
        {
            Version = version;
            var memory = new MemoryStream(bytes);
            MapedWorkbook = Open(memory, version);
        }

        public short GetDataFormat(string format) => MapedWorkbook.CreateDataFormat().GetFormat(format);

        public IEnumerable<ICellStyle> CellStyles => IntegerRange.Create(NumCellStyles).Select(i => GetCellStyleAt((short)i));
        public IEnumerable<CStyle> CStyles => CellStyles.Select(x => new CStyle(this, x));
        public CStyle CStyleAt(short index) => new CStyle(this, GetCellStyleAt(index));
        public CStyle CStyle(Action<CStyleApplier> init) => CStyle(CStyleApplier.Create(init));
        public CStyle CStyle(CStyleApplier applier)
        {
            var find = CStyles.FirstOrDefault(x => x.InterfaceValuesEqual(applier));
            if (find is null)
                return new CStyle(this).Self(_ => applier.Apply(_));
            else return find;
        }

        public IEnumerable<IFont> Fonts => IntegerRange.Create(NumberOfFonts).Select(i => GetFontAt((short)i));
        public IEnumerable<CFont> CFonts => Fonts.Select(x => new CFont(this, x));
        public CFont CFontAt(short index) => new CFont(this, GetFontAt(index));
        public CFont CFont(Action<CFontApplier> init) => CFont(CFontApplier.Create(init));
        internal CFont CFont(CFontApplier applier)
        {
            var find = CFonts.FirstOrDefault(x => x.InterfaceValuesEqual(applier));
            if (find is null)
                return new CFont(this).Self(_ => applier.Apply(_));
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

        public ExcelSheet CreateSheet() => new ExcelSheet(this, MapedWorkbook.CreateSheet());
        public ExcelSheet CloneSheet(string name) => CloneSheet(GetSheetIndex(name));
        public ExcelSheet CloneSheet(int sheetNum) => new ExcelSheet(this, MapedWorkbook.CloneSheet(sheetNum));

        public ExcelSheet CreateSheet(string sheetname) => new ExcelSheet(this, MapedWorkbook.CreateSheet(sheetname));

        public ExcelSheet GetSheet(string name) => new ExcelSheet(this, MapedWorkbook.GetSheet(name));
        public ExcelSheet GetSheetAt(int index) => new ExcelSheet(this, MapedWorkbook.GetSheetAt(index));
        public int GetSheetIndex(ExcelSheet sheet) => GetSheetIndex(sheet.SheetName);

        public byte[] ToArray()
        {
            byte[] bytes;
            using (var memory = new MemoryStream())
            {
                Write(memory);
                bytes = memory.ToArray();
            }
            return bytes;
        }

    }
}
