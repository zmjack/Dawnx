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

        internal BookCellStyle(ExcelBook book)
            : this(book, book.MapedWorkbook.CreateCellStyle())
        { }

        internal BookCellStyle(ExcelBook book, ICellStyle cellStyle)
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
        public RGBColor LeftBorderColor
        {
            get => ColorPairUtility.Get(CellStyle, i => i.LeftBorderColor, c => (c as XSSFCellStyle).LeftBorderXSSFColor);
            set => ColorPairUtility.Set(CellStyle, i => i.LeftBorderColor, c => (c as XSSFCellStyle).LeftBorderXSSFColor, value);
        }

        public BorderStyle BorderRight
        {
            get => CellStyle.BorderRight;
            set => CellStyle.BorderRight = value;
        }
        public RGBColor RightBorderColor
        {
            get => ColorPairUtility.Get(CellStyle, i => i.RightBorderColor, c => (c as XSSFCellStyle).RightBorderXSSFColor);
            set => ColorPairUtility.Set(CellStyle, i => i.RightBorderColor, c => (c as XSSFCellStyle).RightBorderXSSFColor, value);
        }

        public BorderStyle BorderTop
        {
            get => CellStyle.BorderTop;
            set => CellStyle.BorderTop = value;
        }
        public RGBColor TopBorderColor
        {
            get => ColorPairUtility.Get(CellStyle, i => i.TopBorderColor, c => (c as XSSFCellStyle).TopBorderXSSFColor);
            set => ColorPairUtility.Set(CellStyle, i => i.TopBorderColor, c => (c as XSSFCellStyle).TopBorderXSSFColor, value);
        }

        public BorderStyle BorderBottom
        {
            get => CellStyle.BorderBottom;
            set => CellStyle.BorderBottom = value;
        }
        public RGBColor BottomBorderColor
        {
            get => ColorPairUtility.Get(CellStyle, i => i.BottomBorderColor, c => (c as XSSFCellStyle).BottomBorderXSSFColor);
            set => ColorPairUtility.Set(CellStyle, i => i.BottomBorderColor, c => (c as XSSFCellStyle).BottomBorderXSSFColor, value);
        }

        public BorderStyle BorderDiagonalLineStyle
        {
            get => CellStyle.BorderDiagonalLineStyle;
            set => CellStyle.BorderDiagonalLineStyle = value;
        }
        public RGBColor BorderDiagonalColor
        {
            get => ColorPairUtility.Get(CellStyle, i => i.BorderDiagonalColor, c => (c as XSSFCellStyle).DiagonalBorderXSSFColor);
            set => ColorPairUtility.Set(CellStyle, i => i.BorderDiagonalColor, c => (c as XSSFCellStyle).DiagonalBorderXSSFColor, value);
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
            get => ColorPairUtility.Get(CellStyle, i => i.FillBackgroundColor, c => (c as XSSFCellStyle).FillBackgroundXSSFColor);
            set => ColorPairUtility.Set(CellStyle, i => i.FillBackgroundColor, c => (c as XSSFCellStyle).FillBackgroundXSSFColor, value);
        }

        public RGBColor FillForegroundColor
        {
            get => ColorPairUtility.Get(CellStyle, i => i.FillForegroundColor, c => (c as XSSFCellStyle).FillForegroundXSSFColor);
            set => ColorPairUtility.Set(CellStyle, i => i.FillForegroundColor, c => (c as XSSFCellStyle).FillForegroundXSSFColor, value);
        }
        #endregion

        #region Font
        public short FontIndex { get; set; }
        public IBookFont Font { get; set; }
        #endregion

        internal bool InterfaceValuesEqual(BookCellStyleApplier obj)
        {
            var instance = obj as IBookCellStyle;
            if (instance is null) return false;

            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookCellStyle).GetProperties().Where(prop => prop.CanWrite);
            return props.All(prop =>
            {
                var left = prop.GetValue(this);
                var right = prop.GetValue(instance);

                if (left is null && right is null) return true;
                else if (left is null && !(right is null)) return false;
                else if (!(left is null) && right is null) return false;
                else return left.Equals(right);
            });
        }

    }
}
