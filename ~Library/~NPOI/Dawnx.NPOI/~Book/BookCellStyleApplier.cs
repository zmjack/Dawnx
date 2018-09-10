using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.NPOI
{
    public class BookCellStyleApplier : IBookCellStyle
    {
        #region Alignment
        public HorizontalAlignment Alignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        #endregion

        #region Border
        public BorderStyle BorderLeft { get; set; } = BorderStyle.None;
        public IndexedColor LeftBorderColor { get; set; } = IndexedColor.Black;

        public BorderStyle BorderRight { get; set; } = BorderStyle.None;
        public IndexedColor RightBorderColor { get; set; } = IndexedColor.Black;

        public BorderStyle BorderTop { get; set; } = BorderStyle.None;
        public IndexedColor TopBorderColor { get; set; } = IndexedColor.Black;

        public BorderStyle BorderBottom { get; set; } = BorderStyle.None;
        public IndexedColor BottomBorderColor { get; set; } = IndexedColor.Black;

        public BorderStyle BorderDiagonalLineStyle { get; set; } = BorderStyle.None;
        public IndexedColor BorderDiagonalColor { get; set; } = IndexedColor.Black;
        public BorderDiagonal BorderDiagonal { get; set; } = BorderDiagonal.None;
        #endregion

        #region Fill
        public FillPattern FillPattern { get; set; } = FillPattern.NoFill;

        public IndexedColor FillBackgroundColor { get; set; } = IndexedColor.Automatic;
        public RGBColor FillBackgroundColorColor { get; set; }

        public IndexedColor FillForegroundColor { get; set; } = IndexedColor.Automatic;
        public RGBColor FillForegroundColorColor { get; set; }
        #endregion

        public void Apply(BookCellStyle style)
        {
            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookCellStyle).GetProperties().Where(prop => prop.CanWrite);
            foreach (var prop in props)
                prop.SetValue(style, prop.GetValue(this));
        }
    }
}
