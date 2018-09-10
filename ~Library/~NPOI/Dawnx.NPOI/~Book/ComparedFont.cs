using NPOI.SS.UserModel;
using System;

namespace Dawnx.NPOI
{
    [Obsolete]
    public class ComparedFont : IFont
    {
        private short _FontHeightInPoints;

        public string FontName { get; set; }
        public double FontHeight { get; set; }
        public short FontHeightInPoints
        {
            get => _FontHeightInPoints;
            set
            {
                _FontHeightInPoints = value;
                FontHeight = value * 20;
            }
        }
        public bool IsItalic { get; set; }
        public bool IsStrikeout { get; set; }
        public short Color { get; set; } = 8;
        public FontSuperScript TypeOffset { get; set; }
        public FontUnderlineType Underline { get; set; }
        public short Charset { get; set; }

        public short Index => throw new NotImplementedException();

        public short Boldweight { get; set; } = (short)FontBoldWeight.Normal;
        public bool IsBold { get; set; }
    }
}