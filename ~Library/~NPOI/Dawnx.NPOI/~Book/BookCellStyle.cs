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
        public ExcelBook Book { get; private set; }
        public ICellStyle CellStyle { get; private set; }

        public BookCellStyle() { }

        public BookCellStyle(ExcelBook book)
            : this(book, book.MapedWorkbook.CreateCellStyle())
        { }

        public BookCellStyle(ExcelBook book, ICellStyle cellStyle)
        {
            Book = book;
            CellStyle = cellStyle;
        }

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
        public IndexedColor LeftBorderColor
        {
            get => (IndexedColor)CellStyle.LeftBorderColor;
            set => CellStyle.LeftBorderColor = (short)value;
        }

        public BorderStyle BorderRight
        {
            get => CellStyle.BorderRight;
            set => CellStyle.BorderRight = value;
        }
        public IndexedColor RightBorderColor
        {
            get => (IndexedColor)CellStyle.RightBorderColor;
            set => CellStyle.RightBorderColor = (short)value;
        }

        public BorderStyle BorderTop
        {
            get => CellStyle.BorderTop;
            set => CellStyle.BorderTop = value;
        }
        public IndexedColor TopBorderColor
        {
            get => (IndexedColor)CellStyle.TopBorderColor;
            set => CellStyle.TopBorderColor = (short)value;
        }

        public BorderStyle BorderBottom
        {
            get => CellStyle.BorderBottom;
            set => CellStyle.BorderBottom = value;
        }
        public IndexedColor BottomBorderColor
        {
            get => (IndexedColor)CellStyle.BottomBorderColor;
            set => CellStyle.BottomBorderColor = (short)value;
        }

        public BorderStyle BorderDiagonalLineStyle
        {
            get => CellStyle.BorderDiagonalLineStyle;
            set => CellStyle.BorderDiagonalLineStyle = value;
        }
        public IndexedColor BorderDiagonalColor
        {
            get => (IndexedColor)CellStyle.BorderDiagonalColor;
            set => CellStyle.BorderDiagonalColor = (short)value;
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

        public IndexedColor FillBackgroundColor
        {
            get => (IndexedColor)CellStyle.FillBackgroundColor;
            set => CellStyle.FillBackgroundColor = (short)value;
        }
        public RGBColor FillBackgroundColorColor
        {
            get => new RGBColor(CellStyle.FillBackgroundColorColor?.RGB);
            set
            {
                switch (Book.Version)
                {
                    case ExcelVersion.Excel2007:
                        var style = (CellStyle as XSSFCellStyle);
                        style.FillBackgroundColorColor = value?.For(_ => new XSSFColor(_.Bytes));
                        break;

                    default: throw new NotSupportedException();
                }
            }
        }

        public IndexedColor FillForegroundColor
        {
            get => (IndexedColor)CellStyle.FillForegroundColor;
            set => CellStyle.FillForegroundColor = (short)value;
        }
        public RGBColor FillForegroundColorColor
        {
            get => new RGBColor(CellStyle.FillForegroundColorColor?.RGB);
            set
            {
                switch (Book.Version)
                {
                    case ExcelVersion.Excel2007:
                        var style = (CellStyle as XSSFCellStyle);
                        style.FillForegroundColorColor = value?.For(_ => new XSSFColor(_.Bytes));
                        break;

                    default: throw new NotSupportedException();
                }
            }
        }
        #endregion

        public bool InterfaceValuesEqual(IBookCellStyle obj)
        {
            var instance = obj as IBookCellStyle;
            if (instance is null) return false;

            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookCellStyle).GetProperties().Where(prop => prop.CanWrite);
            return props.All(prop => prop.GetValue(this).Equals(prop.GetValue(instance)));
        }

    }
}
