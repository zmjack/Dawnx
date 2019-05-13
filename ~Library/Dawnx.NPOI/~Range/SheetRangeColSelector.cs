using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.NPOI
{
    public class SheetRangeColSelector
    {
        private SheetRange Range;

        public SheetRangeColSelector(SheetRange range)
        {
            Range = range;
        }

        public SheetRange this[int? firstCol, int? lastCol]
        {
            get
            {
                if (firstCol is null) firstCol = Range.Start.col;
                if (lastCol is null) lastCol = Range.End.col;

                return new SheetRange(Range.Sheet, (Range.Start.row, firstCol.Value), (Range.End.row, lastCol.Value));
            }
        }

        public SheetRangeGroup Select(params int[] indexes)
        {
            IEnumerable<SheetRange> select()
            {
                foreach (var index in indexes)
                    yield return new SheetRange(Range.Sheet, (Range.Start.row, Range.Start.col + index), (Range.End.row, Range.Start.col + index));
            }
            return new SheetRangeGroup(select());
        }

        public SheetRangeGroup Select(Func<int, bool> indexSelector)
        {
            IEnumerable<SheetRange> select()
            {
                for (int index = Range.Start.col; index <= Range.End.col; index++)
                {
                    if (indexSelector(index))
                        yield return new SheetRange(Range.Sheet, (Range.Start.row, Range.Start.col + index), (Range.End.row, Range.Start.col + index));
                }
            }
            return new SheetRangeGroup(select());
        }

    }
}
