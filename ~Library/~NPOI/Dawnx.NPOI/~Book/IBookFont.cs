using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.NPOI
{
    public interface IBookFont
    {
        string FontName { get; set; }
        short FontSize { get; set; }

        bool IsItalic { get; set; }
        bool IsStrikeout { get; set; }
        bool IsBold { get; set; }

        FontUnderlineType Underline { get; set; }
        
        RGBColor FontColor { get; set; }

        //FontSuperScript TypeOffset { get; set; }
        //short Charset { get; set; }
        //short Index { get; }
        //short Boldweight { get; set; }
    }
}
