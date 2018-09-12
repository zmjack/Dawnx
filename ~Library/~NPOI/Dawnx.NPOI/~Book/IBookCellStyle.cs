using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.NPOI
{
    public interface IBookCellStyle
    {
        #region Alignment
        HorizontalAlignment Alignment { get; set; }
        VerticalAlignment VerticalAlignment { get; set; }
        #endregion

        #region Border
        BorderStyle BorderLeft { get; set; }
        RGBColor LeftBorderColor { get; set; }

        BorderStyle BorderRight { get; set; }
        RGBColor RightBorderColor { get; set; }

        BorderStyle BorderTop { get; set; }
        RGBColor TopBorderColor { get; set; }

        BorderStyle BorderBottom { get; set; }
        RGBColor BottomBorderColor { get; set; }

        BorderStyle BorderDiagonalLineStyle { get; set; }
        RGBColor BorderDiagonalColor { get; set; }
        BorderDiagonal BorderDiagonal { get; set; }
        #endregion

        #region Fill
        FillPattern FillPattern { get; set; }
        RGBColor FillBackgroundColor { get; set; }
        RGBColor FillForegroundColor { get; set; }
        #endregion

        #region Font
        //short FontIndex { get; set; }
        //IBookFont Font { get; set; }
        #endregion

        //TODO: Border Color

        //public short Rotation { get; set; }
        //public bool WrapText { get; set; }
        //public bool IsLocked { get; set; }
        //public bool IsHidden { get; set; }
        //public short FontIndex { get; }
        //public short DataFormat { get; set; }
        //public short Index { get; }
        //public bool ShrinkToFit { get; set; }
        //public short Indention { get; set; }

        //void CloneStyleFrom(ICellStyle source);
        //string GetDataFormatString();
        //IFont GetFont(IWorkbook parentWorkbook);
        //void SetFont(IFont font);

    }
}
