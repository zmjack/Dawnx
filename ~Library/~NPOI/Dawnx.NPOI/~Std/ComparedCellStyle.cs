using NPOI.SS.UserModel;
using System;

namespace Dawnx.NPOI
{
    [Obsolete]
    public class ComparedCellStyle : ICellStyle
    {
        public bool ShrinkToFit { get; set; }
        public short Index => throw new NotImplementedException();
        public short DataFormat { get; set; }
        public short FontIndex { get; set; }
        public bool IsHidden { get; set; }
        public bool IsLocked { get; set; }
        public HorizontalAlignment Alignment { get; set; }
        public bool WrapText { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public short Rotation { get; set; }
        public short Indention { get; set; }
        public BorderStyle BorderLeft { get; set; }
        public BorderStyle BorderRight { get; set; }
        public BorderStyle BorderTop { get; set; }
        public BorderStyle BorderBottom { get; set; }
        public short LeftBorderColor { get; set; } = IndexedColors.Black.Index;
        public short RightBorderColor { get; set; } = IndexedColors.Black.Index;
        public short TopBorderColor { get; set; } = IndexedColors.Black.Index;
        public short BottomBorderColor { get; set; } = IndexedColors.Black.Index;
        public FillPattern FillPattern { get; set; }
        public short FillBackgroundColor { get; set; } = IndexedColors.White.Index;
        public short FillForegroundColor { get; set; } = IndexedColors.White.Index;
        public short BorderDiagonalColor { get; set; }
        public BorderStyle BorderDiagonalLineStyle { get; set; }
        public BorderDiagonal BorderDiagonal { get; set; }
        public IColor FillBackgroundColorColor => throw new NotImplementedException();
        public IColor FillForegroundColorColor => throw new NotImplementedException();

        public void CloneStyleFrom(ICellStyle source) { throw new NotImplementedException(); }
        public string GetDataFormatString() { throw new NotImplementedException(); }
        public IFont GetFont(IWorkbook parentWorkbook) { throw new NotImplementedException(); }
        public void SetFont(IFont font) { throw new NotImplementedException(); }

        public static ComparedCellStyle FullBorder => new ComparedCellStyle
        {
            BorderTop = BorderStyle.Thin,
            BorderRight = BorderStyle.Thin,
            BorderBottom = BorderStyle.Thin,
            BorderLeft = BorderStyle.Thin,
        };

    }
}