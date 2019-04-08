﻿using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.NPOI
{
    public class SheetRange : IEnumerable<SheetCell>
    {
        public ExcelSheet Sheet { get; private set; }
        public (int row, int col) Start { get; private set; }
        public (int row, int col) End { get; private set; }
        public string Name => $"{Sheet.GetCellName(Start)}:{Sheet.GetCellName(End)}";
        public int RowLength => End.row - Start.row + 1;
        public int ColumnLengh => End.col - Start.col + 1;
        public SheetCell Cell => Sheet[Start];

        public bool IsSingleRow => RowLength == 1;
        public bool IsSingleColumn => ColumnLengh == 1;

        public bool IsInRange(SheetCell cell)
        {
            return Start.row <= cell.RowIndex && cell.RowIndex <= End.row
                && Start.col <= cell.ColumnIndex && cell.ColumnIndex <= End.col;
        }
        public bool IsDefinitionCell(SheetCell cell) => cell.RowIndex == Start.row && cell.ColumnIndex == Start.col;

        public SheetRange(ExcelSheet sheet, (int row, int col) start, (int row, int col) end)
        {
            Sheet = sheet;
            Start = start;
            End = end;
        }

        public void SetCellStyle(ICellStyle style)
        {
            for (int row = Start.row; row <= End.row; row++)
                for (int col = Start.col; col <= End.col; col++)
                    Sheet[(row, col)].SetCellStyle(style);
        }
        public void SetCStyle(CStyle style) => SetCellStyle(style.CellStyle);
        public void SetCStyle(Action<CStyleApplier> initApplier)
            => SetCellStyle(Sheet.Book.CStyle(initApplier).CellStyle);

        public SheetRange SelectColunm(int startOffsetCol)
        {
            return new SheetRange(Sheet, (Start.row, Start.col + startOffsetCol), (End.row, Start.col + startOffsetCol));
        }

        public SheetRange SelectColunms(int startOffsetCol, int endOffsetCol)
        {
            return new SheetRange(Sheet, (Start.row, Start.col + startOffsetCol), (End.row, Start.col + endOffsetCol));
        }

        public void Merge()
        {
            for (int row = Start.row; row <= End.row; row++)
            {
                for (int col = Start.col; col <= End.col; col++)
                {
                    if (!(row == Start.row && col == Start.col))
                        Sheet[(row, col)].SetValue("");
                }
            }

            var region = new CellRangeAddress(Start.row, End.row, Start.col, End.col);
            Sheet.AddMergedRegion(region);
        }

        /// <summary>
        /// Merge cells that has same value.
        ///     You can use [[ ]] to identifier cells, the result of merged cell will ignore the value which is in [[ ]].
        /// </summary>
        /// <param name="offsetCols"></param>
        public void SmartMerge(params int[] offsetCols)
        {
            var col = Start.col + offsetCols[0];

            string take = null;
            int mergeStart = Start.row;

            for (int takeRow = mergeStart; takeRow <= End.row; takeRow++)
            {
                var value = Sheet[(takeRow, col)].GetValue().ToString();
                if (value != take)
                {
                    if (takeRow - mergeStart > 1)
                        SmartColMerge(mergeStart, takeRow - 1, col, offsetCols);

                    mergeStart = takeRow;
                    take = value;
                }
                else continue;
            }

            if (End.row > mergeStart)
                SmartColMerge(mergeStart, End.row, col, offsetCols);

            //TODO: Remove identifier(type will be changed -> unchange)
            var regex_matchId = new Regex(@"^\[\[.+?\]\](.*)$");
            foreach (var colIndex in offsetCols)
            {
                foreach (var cell in SelectColunm(colIndex))
                {
                    var value = cell.GetValue();
                    if (value is string)
                    {
                        var match = regex_matchId.Match(value as string);
                        if (match.Success)
                        {
                            if (double.TryParse(match.Groups[1].Value, out double dValue))
                                cell.SetValue(dValue);
                            else cell.SetValue(match.Groups[1].Value);
                        }
                    }
                }
            }
        }

        private void SmartColMerge(int mergeStart, int mergeEnd, int col, int[] offsetCols)
        {
            new SheetRange(Sheet, (mergeStart, col), (mergeEnd, col)).Merge();
            if (offsetCols.Length > 1)
            {
                new SheetRange(Sheet, (mergeStart, col + offsetCols[1]), (mergeEnd, End.col))
                    .SmartMerge(offsetCols.Slice(1).Select(_col => _col - (offsetCols[1] - offsetCols[0])).ToArray());
            }
        }

        public SheetCell this[(int offsetRow, int offsetCol) pos]
            => Sheet[(Start.row + pos.offsetRow, Start.col + pos.offsetCol)];

        public IEnumerable<SheetRange> GetRows()
        {
            for (int row = Start.row; row <= End.row; row++)
                yield return new SheetRange(Sheet, (row, Start.col), (row, End.col));
        }

        public IEnumerator<SheetCell> GetEnumerator()
        {
            for (int row = Start.row; row <= End.row; row++)
                for (int col = Start.col; col <= End.col; col++)
                    yield return new SheetCell(Sheet, Sheet[(row, col)]);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}