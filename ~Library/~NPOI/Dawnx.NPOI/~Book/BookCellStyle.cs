using Dawnx.Utilities;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.NPOI
{
    public class BookCellStyle : IBookCellStyle
    {
        public ExcelBook Book { get; }
        public ICellStyle CellStyle { get; }

        internal BookCellStyle(ExcelBook book)
            : this(book, book.MapedWorkbook.CreateCellStyle())
        { }

        internal BookCellStyle(ExcelBook book, ICellStyle cellStyle)
        {
            Book = book;
            CellStyle = cellStyle;
        }

        public short Index => CellStyle.Index;

        #region Alignment
        public HorizontalAlignment Alignment
        {
            get => CellStyle.Alignment;
            set => CellStyle.Alignment = value;
        }
        public VerticalAlignment VerticalAlignment
        {
            get => CellStyle.VerticalAlignment;
            set => CellStyle.VerticalAlignment = value;
        }
        #endregion

        #region Border
        public BorderStyle BorderLeft
        {
            get => CellStyle.BorderLeft;
            set => CellStyle.BorderLeft = value;
        }
        public RGBColor LeftBorderColor
        {
            get => (CellStyle as XSSFCellStyle)?.LeftBorderXSSFColor?.RGB.For(_ => new RGBColor(_))
                ?? RGBColor.ParseIndexed(CellStyle.LeftBorderColor);
            set
            {
                var xssf = (CellStyle as XSSFCellStyle)?
                    .Self(_ => _.SetLeftBorderColor(new XSSFColor(value.Bytes))).For(_ => true) ?? false;
                if (!xssf) CellStyle.LeftBorderColor = value.Index;
            }
        }

        public BorderStyle BorderRight
        {
            get => CellStyle.BorderRight;
            set => CellStyle.BorderRight = value;
        }
        public RGBColor RightBorderColor
        {
            get => (CellStyle as XSSFCellStyle)?.RightBorderXSSFColor?.RGB.For(_ => new RGBColor(_))
                ?? RGBColor.ParseIndexed(CellStyle.RightBorderColor);
            set
            {
                var xssf = (CellStyle as XSSFCellStyle)?
                    .Self(_ => _.SetRightBorderColor(new XSSFColor(value.Bytes))).For(_ => true) ?? false;
                if (!xssf) CellStyle.RightBorderColor = value.Index;
            }
        }

        public BorderStyle BorderTop
        {
            get => CellStyle.BorderTop;
            set => CellStyle.BorderTop = value;
        }
        public RGBColor TopBorderColor
        {
            get => (CellStyle as XSSFCellStyle)?.TopBorderXSSFColor?.RGB.For(_ => new RGBColor(_))
                ?? RGBColor.ParseIndexed(CellStyle.TopBorderColor);
            set
            {
                var xssf = (CellStyle as XSSFCellStyle)?
                    .Self(_ => _.SetTopBorderColor(new XSSFColor(value.Bytes))).For(_ => true) ?? false;
                if (!xssf) CellStyle.TopBorderColor = value.Index;
            }
        }

        public BorderStyle BorderBottom
        {
            get => CellStyle.BorderBottom;
            set => CellStyle.BorderBottom = value;
        }
        public RGBColor BottomBorderColor
        {
            get => (CellStyle as XSSFCellStyle)?.BottomBorderXSSFColor?.RGB.For(_ => new RGBColor(_))
                ?? RGBColor.ParseIndexed(CellStyle.BottomBorderColor);
            set
            {
                var xssf = (CellStyle as XSSFCellStyle)?
                    .Self(_ => _.SetBottomBorderColor(new XSSFColor(value.Bytes))).For(_ => true) ?? false;
                if (!xssf) CellStyle.BottomBorderColor = value.Index;
            }
        }

        public BorderStyle BorderDiagonalLineStyle
        {
            get => CellStyle.BorderDiagonalLineStyle;
            set => CellStyle.BorderDiagonalLineStyle = value;
        }
        public RGBColor BorderDiagonalColor
        {
            get => (CellStyle as XSSFCellStyle)?.DiagonalBorderXSSFColor?.RGB.For(_ => new RGBColor(_))
                ?? RGBColor.ParseIndexed(CellStyle.BorderDiagonalColor);
            set
            {
                var xssf = (CellStyle as XSSFCellStyle)?
                    .Self(_ => _.SetDiagonalBorderColor(new XSSFColor(value.Bytes))).For(_ => true) ?? false;
                if (!xssf) CellStyle.BorderDiagonalColor = value.Index;
            }
        }
        public BorderDiagonal BorderDiagonal
        {
            get => CellStyle.BorderDiagonal;
            set => CellStyle.BorderDiagonal = value;
        }
        #endregion

        #region Fill
        public FillPattern FillPattern
        {
            get => CellStyle.FillPattern;
            set => CellStyle.FillPattern = value;
        }

        public RGBColor FillBackgroundColor
        {
            get => (CellStyle as XSSFCellStyle)?.FillBackgroundXSSFColor?.RGB.For(_ => new RGBColor(_))
                ?? RGBColor.ParseIndexed(CellStyle.FillBackgroundColor);
            set
            {
                var xssf = (CellStyle as XSSFCellStyle)?
                    .Self(_ => _.FillBackgroundXSSFColor = new XSSFColor(value.Bytes)).For(_ => true) ?? false;
                if (!xssf) CellStyle.FillBackgroundColor = value.Index;
            }
        }

        public RGBColor FillForegroundColor
        {
            get => (CellStyle as XSSFCellStyle)?.FillForegroundXSSFColor?.RGB.For(_ => new RGBColor(_))
                ?? RGBColor.ParseIndexed(CellStyle.FillForegroundColor);
            set
            {
                var xssf = (CellStyle as XSSFCellStyle)?
                    .Self(_ => _.FillForegroundXSSFColor = new XSSFColor(value.Bytes)).For(_ => true) ?? false;
                if (!xssf) CellStyle.FillForegroundColor = value.Index;
            }
        }
        #endregion

        #region Font
        public BookFont Font
        {
            get => Book.BookFontAt(CellStyle.FontIndex);
            set => CellStyle.SetFont(value.Font);
        }
        #endregion

        #region DataFormat
        public string DataFormat
        {
            get => CellStyle.GetDataFormatString();
            set => CellStyle.DataFormat = Book.GetDataFormat(value);
        }
        #endregion

        #region Others
        public short Rotation
        {
            get => CellStyle.Rotation;
            set => CellStyle.Rotation = value;
        }
        public short Indention
        {
            get => CellStyle.Indention;
            set => CellStyle.Indention = value;
        }
        public bool WrapText
        {
            get => CellStyle.WrapText;
            set => CellStyle.WrapText = value;
        }
        public bool IsLocked
        {
            get => CellStyle.IsLocked;
            set => CellStyle.IsLocked = value;
        }
        public bool IsHidden
        {
            get => CellStyle.IsHidden;
            set => CellStyle.IsHidden = value;
        }
        public bool ShrinkToFit
        {
            get => CellStyle.ShrinkToFit;
            set => CellStyle.ShrinkToFit = value;
        }
        #endregion

        internal bool InterfaceValuesEqual(BookCellStyleApplier obj)
        {
            var instance = obj as IBookCellStyle;
            if (instance is null) return false;

            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookCellStyle).GetProperties().Where(prop => prop.CanWrite);
            return props.All(prop => CompareUtility.UsingEquals(prop.GetValue(this), prop.GetValue(instance)))
                && Font.InterfaceValuesEqual(obj.Font);
        }

    }
}
