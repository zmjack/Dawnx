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
        ExcelColor LeftBorderColor { get; set; }

        BorderStyle BorderRight { get; set; }
        ExcelColor RightBorderColor { get; set; }

        BorderStyle BorderTop { get; set; }
        ExcelColor TopBorderColor { get; set; }

        BorderStyle BorderBottom { get; set; }
        ExcelColor BottomBorderColor { get; set; }

        BorderStyle BorderDiagonalLineStyle { get; set; }
        ExcelColor BorderDiagonalColor { get; set; }
        BorderDiagonal BorderDiagonal { get; set; }
        #endregion

        //public short FillForegroundColor { get; set; }
        //public short FillBackgroundColor { get; set; }
        //public FillPattern FillPattern { get; set; }
        //public IColor FillBackgroundColorColor { get; }
        //public IColor FillForegroundColorColor { get; }
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

        //public static implicit operator ICellStyle(SheetCellStyle instance)
        //{
        //    var cellStyles = Book.CellStyles;

        //    var findStyle = CellStyles.FirstOrDefault(style =>
        //    {
        //        if (props.All(prop => prop.GetValue(style).Equals(prop.GetValue(compared)))
        //            && style.FontIndex == compared.FontIndex)
        //            return true;
        //        else return false;
        //    });
        //}

    }
}
