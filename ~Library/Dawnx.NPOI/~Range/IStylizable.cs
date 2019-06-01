using NPOI.SS.UserModel;
using System;

namespace Dawnx.NPOI
{
    public interface IStylizable
    {
        void SetCellStyle(ICellStyle style);
        void SetCStyle(CStyle style);
        void SetCStyle(Action<CStyleApplier> initApplier);
    }
}
