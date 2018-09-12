﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
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

        public short GetDataFormat(string format) => MapedWorkbook.CreateDataFormat().For(_ => _.GetFormat(format));

        public ICellStyle[] CellStyles
            => Range.Create(NumCellStyles).Select(i => GetCellStyleAt((short)i)).ToArray();
        public ICellStyle GetCellStyle(ComparedCellStyle compared)
        {
            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(ICellStyle).GetProperties().Where(prop => prop.CanWrite);

            var findStyle = CellStyles.FirstOrDefault(style =>
            {
                if (props.All(prop => prop.GetValue(style).Equals(prop.GetValue(compared)))
                    && style.FontIndex == compared.FontIndex)
                    return true;
                else return false;
            });

            if (findStyle is null)
            {
                var newStyle = MapedWorkbook.CreateCellStyle().Self(_ =>
                {
                    _.SetFont(GetFontAt(compared.FontIndex));
                    _.BorderLeft = compared.BorderLeft;
                    _.BorderDiagonal = compared.BorderDiagonal;
                    _.BorderDiagonalLineStyle = compared.BorderDiagonalLineStyle;
                    _.BorderDiagonalColor = compared.BorderDiagonalColor;
                    _.FillPattern = compared.FillPattern;
                    _.FillForegroundColor = compared.FillForegroundColor;
                    _.FillBackgroundColor = compared.FillBackgroundColor;
                    _.BottomBorderColor = compared.BottomBorderColor;
                    _.TopBorderColor = compared.TopBorderColor;
                    _.RightBorderColor = compared.RightBorderColor;
                    _.LeftBorderColor = compared.LeftBorderColor;
                    _.BorderBottom = compared.BorderBottom;
                    _.BorderTop = compared.BorderTop;
                    _.BorderRight = compared.BorderRight;
                    _.Rotation = compared.Rotation;
                    _.VerticalAlignment = compared.VerticalAlignment;
                    _.WrapText = compared.WrapText;
                    _.Alignment = compared.Alignment;
                    _.IsLocked = compared.IsLocked;
                    _.IsHidden = compared.IsHidden;
                    _.DataFormat = compared.DataFormat;
                    _.ShrinkToFit = compared.ShrinkToFit;
                    _.Indention = compared.Indention;
                });
                return newStyle;
            }
            else return findStyle;
        }

        public BookCellStyle[] BookCellStyles => CellStyles.Select(x => new BookCellStyle(this, x)).ToArray();
        public BookCellStyle CreateBookCellStyle(Action<BookCellStyle> init)
            => new BookCellStyle(this).Self(_ => init(_));
        public BookCellStyle GetBookCellStyle(Action<BookCellStyleApplier> initApplier)
        {
            var compared = new BookCellStyleApplier().Self(_ => initApplier(_));
            return BookCellStyles.FirstOrDefault(x => x.InterfaceValuesEqual(compared));
        }
        public BookCellStyle BookCellStyle(Action<BookCellStyleApplier> initApplier)
        {
            var compared = new BookCellStyleApplier().Self(_ => initApplier(_));

            var find = BookCellStyles.FirstOrDefault(x => x.InterfaceValuesEqual(compared));
            if (find is null)
                return new BookCellStyle(this).Self(_ => compared.Apply(_));
            else return find;
        }

        public IFont[] Fonts
            => Range.Create(NumberOfFonts).Select(i => GetFontAt((short)i)).ToArray();
        public IFont GetFont(ComparedFont compared)
        {
            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IFont).GetProperties().Where(prop => prop.CanWrite);

            var findFont = Fonts.FirstOrDefault(style =>
            {
                if (props.All(prop => prop.GetValue(style).Equals(prop.GetValue(compared))))
                    return true;
                else return false;
            });

            if (findFont is null)
            {
                var newFont = MapedWorkbook.CreateFont().Self(_ =>
                {
                    _.FontName = compared.FontName;
                    _.FontHeightInPoints = compared.FontHeightInPoints;
                    _.IsItalic = compared.IsItalic;
                    _.IsStrikeout = compared.IsStrikeout;
                    _.Color = compared.Color;
                    _.TypeOffset = compared.TypeOffset;
                    _.Underline = compared.Underline;
                    _.Charset = compared.Charset;
                    _.Boldweight = compared.Boldweight;
                    _.IsBold = compared.IsBold;
                });
                return newFont;
            }
            else return findFont;
        }

        public BookFont[] BookFonts => Fonts.Select(x => new BookFont(this, x)).ToArray();
        public BookFont CreateBookFont(Action<BookFont> init)
            => new BookFont(this).Self(_ => init(_));
        public BookFont GetBookFont(Action<BookFontApplier> initApplier)
        {
            var compared = new BookFontApplier().Self(_ => initApplier(_));
            return BookFonts.FirstOrDefault(x => x.InterfaceValuesEqual(compared));
        }
        public BookFont BookFont(Action<BookFontApplier> initApplier)
        {
            var compared = new BookFontApplier().Self(_ => initApplier(_));

            var find = BookFonts.FirstOrDefault(x => x.InterfaceValuesEqual(compared));
            if (find is null)
                return new BookFont(this).Self(_ => compared.Apply(_));
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