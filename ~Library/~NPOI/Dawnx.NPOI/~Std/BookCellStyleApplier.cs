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
        public BorderStyle BorderLeft { get; set; }
        public ExcelColor LeftBorderColor { get; set; }

        public BorderStyle BorderRight { get; set; }
        public ExcelColor RightBorderColor { get; set; }

        public BorderStyle BorderTop { get; set; }
        public ExcelColor TopBorderColor { get; set; }

        public BorderStyle BorderBottom { get; set; }
        public ExcelColor BottomBorderColor { get; set; }

        public BorderStyle BorderDiagonalLineStyle { get; set; }
        public ExcelColor BorderDiagonalColor { get; set; }
        public BorderDiagonal BorderDiagonal { get; set; }
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
