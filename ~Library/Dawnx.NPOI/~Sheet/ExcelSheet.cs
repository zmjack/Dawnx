using Dawnx.Ranges;
using Dawnx.Reflection;
using Dawnx.Sequences;
using NPOI.SS.UserModel;
using NStandard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dawnx.NPOI
{
    public partial class ExcelSheet
    {
        public const int EXCEL_WIDTH_PER_PX = 8;
        public const int AUTO_SIZE_PADDING_PX = 21;
        public const int COLUMN_BORDER_PX = 3;

        public ISheet MapedSheet { get; private set; }
        public ExcelBook Book { get; private set; }
        public (int row, int col) Cursor;

        public ExcelSheet(ExcelBook excel, ISheet sheet)
        {
            if (sheet is null)
                throw new ArgumentException("Cannot find sheet.");

            Book = excel;
            MapedSheet = sheet;
            Cursor = (0, 0);
        }

        public (int row, int col) GetCellPos(string cellName)
        {
            var regex = new Regex(@"^([A-Z]+)(\d+)$");
            var match = regex.Match(cellName);
            if (match.Success)
                return (GetRow(match.Groups[2].Value), GetCol(match.Groups[1].Value));
            else throw new FormatException($"Illegal cell format：{cellName}。");
        }
        public string GetCellName((int row, int col) pos) => $"{GetColName(pos.col)}{GetRowName(pos.row)}";

        public void SetCursor(string cell) => Cursor = GetCellPos(cell);

        public void CellMove() => Cursor.col++;
        public void NextLine() => Cursor.row++;

        public SheetCell this[(int row, int col) pos]
        {
            get
            {
                var irow = GetRow(pos.row);
                if (irow is null)
                    irow = CreateRow(pos.row);

                var icol = irow.GetCell(pos.col);
                if (icol is null)
                    icol = irow.CreateCell(pos.col);

                return new SheetCell(this, icol);
            }
        }
        public SheetCell this[string cellName] => this[GetCellPos(cellName)];

        public SheetRange this[(int row, int col) start, (int row, int col) end] => new SheetRange(this, start, end);
        public SheetRange this[string start, string end] => new SheetRange(this, GetCellPos(start), GetCellPos(end));

        public SheetRange Print(object[,] values, bool reserveCursor = false)
        {
            var startRow = Cursor.row;
            var rowLength = values.GetLength(0);
            var colLength = values.GetLength(1);

            for (var row = 0; row < rowLength; row++)
            {
                for (var col = 0; col < colLength; col++)
                {
                    var valueObj = values[row, col];
                    if (valueObj is null) continue;

                    if (valueObj is string && (valueObj as string).StartsWith("="))
                    {
                        //TODO: Analysis formula in same row
                        this[(Cursor.row + row, Cursor.col + col)].SetValue(valueObj);
                    }
                    else this[(Cursor.row + row, Cursor.col + col)].SetValue(valueObj);
                }
            }

            if (!reserveCursor)
                Cursor.row += values.GetLength(0);

            return new SheetRange(this,
                (startRow, Cursor.col),
                (startRow + rowLength - 1, Cursor.col + colLength - 1));
        }

        public SheetRange Print(object[][] values, bool reserveCursor = false)
        {
            var startRow = Cursor.row;
            var rowLength = values.Length;
            var colLength = values.Any() ? values.Max(values1 => values1.Length) : 0;

            for (int row = 0; row < rowLength; row++)
            {
                for (int col = 0; col < values[row].Length; col++)
                {
                    var valueObj = values[row][col];
                    if (valueObj is null) continue;

                    this[(Cursor.row + row, Cursor.col + col)].SetValue(valueObj);
                }
            }

            if (!reserveCursor)
                Cursor.row += values.Length;

            return new SheetRange(this,
                (startRow, Cursor.col),
                (startRow + rowLength - 1, Cursor.col + colLength - 1));
        }

        public SheetRange PrintLine(params object[] values) => Print(values, false);
        public SheetRange Print(object[] values, bool reserveCursor)
        {
            var startRow = Cursor.row;

            for (int col = 0; col < values.Length; col++)
            {
                var valueObj = values[col];
                if (valueObj is null) continue;

                this[(Cursor.row, Cursor.col + col)].SetValue(valueObj);
            }

            if (!reserveCursor)
                Cursor.row++;

            return new SheetRange(this, (startRow, Cursor.col), (startRow, Cursor.col + values.Length - 1));
        }

        public SheetRange Print(DataTable table, bool reserveCursor = false)
        {
            var range1 = Print(table.Columns.Cast<DataColumn>().Select(a => a.ColumnName).ToArray(), reserveCursor);
            var range2 = Print((from DataRow row in table.Select()
                                select row.ItemArray.ToArray()).ToArray(), reserveCursor);

            return new SheetRange(this, range1.Start, range2.End);
        }

        public TModel[] Fetch<TModel>(string startCell, Expression<Func<TModel, object>> includes = null, Predicate<int> rowSelector = null)
            where TModel : new()
        {
            var converter = new BasicConverter();
            var ret = new List<TModel>();
            var pos = GetCellPos(startCell);
            (int row, int col) = (pos.row, pos.col);
            PropertyInfo[] props;

            var propNames = new string[0];
            if (includes != null)
            {
                switch (includes.Body)
                {
                    case MemberExpression exp:
                        propNames = new[] { exp.Member.Name };
                        break;

                    case NewExpression exp:
                        propNames = exp.Members.Select(x => x.Name).ToArray();
                        break;

                    default:
                        throw new NotSupportedException("This argument 'includes' must be MemberExpression or NewExpression.");
                }
            }

            if (propNames.Any())
            {
                props = typeof(TModel).GetProperties()
                    .Where(prop => prop.CanWrite && propNames.Contains(prop.Name))
                    .OrderBy(prop => propNames.IndexOf(prop.Name)).ToArray();
            }
            else props = typeof(TModel).GetProperties().Where(prop => prop.CanWrite).ToArray();

            for (int rowOffset = 0; pos.row + rowOffset <= MapedSheet.LastRowNum; rowOffset++)
            {
                if (rowSelector != null)
                {
                    if (!rowSelector(rowOffset)) continue;
                }

                var @break = true;
                for (int i = 0; i < props.Length; i++)
                {
                    var cell = this[(pos.row + rowOffset, pos.col + i)];
                    var cellType = cell.IsMergedCell ? cell.MergedRange.Cell.CellType : cell.CellType;
                    if (cellType != CellType.Blank)
                    {
                        @break = false;
                        break;
                    }
                }
                if (@break) break;

                var item = new TModel();
                foreach (var kv in props.AsKvPairs())
                {
                    var propInfo = kv.Value;
                    var cell = this[(row + rowOffset, pos.col + kv.Key)];

                    if (propInfo.PropertyType == typeof(DateTime) || propInfo.PropertyType == typeof(DateTime?))
                    {
                        if (cell.CellType == CellType.Blank)
                        {
                            if (propInfo.PropertyType == typeof(DateTime?))
                                propInfo.SetValue(item, converter.Convert(propInfo.PropertyType, null, propInfo));
                            else propInfo.SetValue(item, converter.Convert(propInfo.PropertyType, default(DateTime), propInfo));
                        }
                        else
                        {
                            var value = cell.IsMergedCell ? cell.MergedRange.Cell.DateTime : cell.DateTime;
                            propInfo.SetValue(item, converter.Convert(propInfo.PropertyType, value, propInfo));
                        }
                    }
                    else
                    {
                        var value = cell.IsMergedCell ? cell.MergedRange.Cell.GetValue() : cell.GetValue();
                        propInfo.SetValue(item, converter.Convert(propInfo.PropertyType, value, propInfo));
                    }
                }
                ret.Add(item);
            }

            return ret.ToArray();
        }

        public DataTable Fetch(string startCell, bool isFirstRowTitle)
        {
            var pos = GetCellPos(startCell);
            var colLength = 0;

            var @continue = true;
            for (int colOffset = 0; @continue; colOffset++)
            {
                if (this[(pos.row, pos.col + colOffset)]
                    .For(x => x.MapedCell.CellType != CellType.Blank && !x.String.IsNullOrWhiteSpace()))
                {
                    colLength++;
                }
                else break;
            }

            return Fetch(pos, isFirstRowTitle, new IntegerRange(colLength).Select(i => typeof(string)).ToArray());
        }
        public DataTable Fetch(string startCell, bool isFirstRowTitle, Type[] colTypes)
        {
            var pos = GetCellPos(startCell);
            return Fetch(pos, isFirstRowTitle, colTypes);
        }
        public DataTable Fetch((int row, int col) pos, bool isFirstRowTitle, Type[] colTypes)
        {
            var ret = new DataTable();
            ret.Columns.Then(x => x.AddRange(colTypes.Select(colType => new DataColumn("", colType)).ToArray()));

            (int row, int col) dataStart;
            if (isFirstRowTitle)
            {
                for (int i = 0; i < colTypes.Length; i++)
                    ret.Columns[i].ColumnName = this[(pos.row, pos.col + i)].String;
                dataStart = (pos.row + 1, pos.col);
            }
            else dataStart = (pos.row, pos.col);

            var @continue = true;
            for (int rowOffset = 0; @continue; rowOffset++)
            {
                var allEmpty = true;
                for (int i = 0; i < colTypes.Length; i++)
                {
                    if (this[(pos.row + rowOffset, pos.col + i)].MapedCell.CellType != CellType.Blank)
                    {
                        allEmpty = false;
                        break;
                    }
                }

                if (!allEmpty)
                {
                    var rowValues = colTypes.Select((colType, i) =>
                    {
                        var cell = this[(dataStart.row + rowOffset, pos.col + i)];

                        if (colType == typeof(DateTime))
                            return cell.DateTime;
                        else return cell.GetValue();
                    }).ToArray();
                    ret.Rows.Add(rowValues);
                }
                else @continue = false;
            }

            return ret;
        }

        public void SetWidth(string colName, double width) => SetWidth(GetCol(colName), width);
        public void SetWidth(int columnIndex, double width) => MapedSheet.SetColumnWidth(columnIndex, (int)Math.Ceiling(255.86 * width + 184.27));

        public void SetHeight(string rowName, float height) => this[(GetRow(rowName), 1)].SetHeight(height);
        public void SetHeight(int rowIndex, float height) => this[(rowIndex, 1)].SetHeight(height);

        public int GetWidth(int columnIndex) => (int)Math.Ceiling((MapedSheet.GetColumnWidth(columnIndex) - 184.27) / 255.86);

        public void AutoSize(IntegerRange range) => AutoSize(range.ToArray());
        public void AutoSize(LetterRange range) => AutoSize(range.ToArray());
        public void AutoSize(params string[] columns) => AutoSize(columns.Select(x => LetterSequence.GetNumber(x)).ToArray());
        public void AutoSize(params int[] columns)
        {
            foreach (var col in columns)
            {
                int defaultWidth = GetWidth(col);
                var exceptRows = EnumerableEx.Concat(
                    MergedRanges
                        .Where(x => x.Start.col < col && col <= x.End.col)
                        .SelectMany(x => new int[x.End.row - x.Start.row + 1].Let(i => x.Start.row + i))
                        .ToArray(),
                    MergedRanges
                        .Where(x => x.Start.col == col)
                        .SelectMany(x => new int[x.End.row - x.Start.row].Let(i => x.Start.row + i + 1))
                        .ToArray())
                    .ToArray();

                var rowNumbers = new int[LastRowNum + 1].Let(i => i).Where(row => !exceptRows.Contains(row));
                var widths = rowNumbers.Select(row =>
                {
                    var cell = this[(row, col)];
                    if (cell.MergedRange?.For(_ => !_.IsSingleColumn || !_.IsDefinitionCell(cell)) ?? false) return 0;

                    var cstyle = cell.GetCStyle();
                    var value = cell.GetValue();

                    string valueString;
                    if (value is double)
                        valueString = ((double)value).ToString(cstyle.DataFormat);
                    else if (value is DateTime)
                        valueString = ((DateTime)value).ToString(cstyle.DataFormat);
                    else valueString = value?.ToString() ?? "";

                    using (var bitmap = new Bitmap(1, 1))
                    using (var graphics = Graphics.FromImage(new Bitmap(1, 1)))
                    {
                        var fontSize = graphics.MeasureString(valueString, new Font(cstyle.Font.FontName, cstyle.Font.FontSize));
                        var width = fontSize.Width > 0 ? (int)((COLUMN_BORDER_PX + AUTO_SIZE_PADDING_PX + fontSize.Width) / EXCEL_WIDTH_PER_PX) : 0;
                        return width;
                    }
                });

                SetWidth(col, new[] { defaultWidth }.Concat(widths).Max());
            }
        }

        public IEnumerable<SheetRange> MergedRanges
        {
            get
            {
                return IntegerRange.Create(NumMergedRegions).Select(
                    i => GetMergedRegion(i).For(x => new SheetRange(this, (x.FirstRow, x.FirstColumn), (x.LastRow, x.LastColumn))));
            }
        }

    }
}