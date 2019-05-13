using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.NPOI
{
    public class SheetRangeRowSelector
    {
        private SheetRange Range;

        public SheetRangeRowSelector(SheetRange range)
        {
            Range = range;
        }

        public SheetRange this[int? firstRow, int? lastRow]
        {
            get
            {
                if (firstRow is null) firstRow = Range.Start.row;
                if (lastRow is null) lastRow = Range.End.row;

                return new SheetRange(Range.Sheet, (firstRow.Value, Range.Start.col), (lastRow.Value, Range.End.col));
            }
        }

        public SheetRangeGroup Select(params int[] indexes)
        {
            IEnumerable<SheetRange> select()
            {
                foreach (var index in indexes)
                    yield return new SheetRange(Range.Sheet, (Range.Start.row + index, Range.Start.col), (Range.Start.row + index, Range.End.col));
            }
            return new SheetRangeGroup(select());
        }

        public SheetRangeGroup Select(Func<int, bool> indexSelector)
        {
            IEnumerable<SheetRange> select()
            {
                for (int index = Range.Start.row; index <= Range.End.row; index++)
                {
                    if (indexSelector(index))
                        yield return new SheetRange(Range.Sheet, (Range.Start.row + index, Range.Start.col), (Range.Start.row + index, Range.End.col));
                }
            }
            return new SheetRangeGroup(select());
        }

    }
}
