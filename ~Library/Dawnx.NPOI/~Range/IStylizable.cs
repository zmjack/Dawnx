using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.NPOI
{
    public interface IStylizable
    {
        void SetCellStyle(ICellStyle style);
        void SetCStyle(CStyle style);
        void SetCStyle(Action<CStyleApplier> initApplier);
    }
}
