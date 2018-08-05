﻿using Dawnx.Reflection;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dawnx.NPOI
{
    public partial class ExcelSheet
    {
        public ISheet MapedSheet { get; private set; }
        public ExcelBook Book { get; private set; }
        public (int row, int col) Cursor;

        public ExcelSheet(ExcelBook excel, ISheet sheet)
        {
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
                if (irow == null)
                    irow = CreateRow(pos.row);

                var icol = irow.GetCell(pos.col);
                if (icol == null)
                    icol = irow.CreateCell(pos.col);

                return new SheetCell(this, icol);
            }
        }
        public SheetCell this[string cellName]
        {
            get
            {
                var pos = GetCellPos(cellName);
                return this[(pos.row, pos.col)];
            }
        }

        public SheetRange this[(int row, int col) start, (int row, int col) end]
            => new SheetRange(this, start, end);
        public SheetRange this[string start, string end]
            => new SheetRange(this, GetCellPos(start), GetCellPos(end));

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
                    if (valueObj == null) continue;

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
                    if (valueObj == null) continue;

                    this[(Cursor.row + row, Cursor.col + col)].SetValue(valueObj);
                }
            }

            if (!reserveCursor)
                Cursor.row += values.Length;

            return new SheetRange(this,
                (startRow, Cursor.col),
                (startRow + rowLength - 1, Cursor.col + colLength - 1));
        }

        public SheetRange Print(params object[] values) => Print(values, false);
        public SheetRange Print(object[] values, bool reserveCursor)
        {
            var startRow = Cursor.row;

            for (int col = 0; col < values.Length; col++)
            {
                var valueObj = values[col];
                if (valueObj == null) continue;

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

        public TModel[] Fetch<TModel>(string startCell, Expression<Func<TModel, object>> includes = null)
            where TModel : new()
        {
            var converter = new DefaultBasicTypeConverter(false);
            var ret = new List<TModel>();
            var pos = GetCellPos(startCell);
            (int row, int col) dataStart = (pos.row, pos.col);
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
                var @break = true;
                for (int i = 0; i < props.Length; i++)
                {
                    if (this[(pos.row + rowOffset, pos.col + i)].MapedCell.CellType != CellType.Blank)
                    {
                        @break = false;
                        break;
                    }
                }
                if (@break) break;

                var item = new TModel();
                props.Each((prop, i) =>
                {
                    var cell = this[(dataStart.row + rowOffset, pos.col + i)];
                    prop.SetValue(item, converter.Convert(prop, cell.GetValue()));
                });
                ret.Add(item);
            }

            return ret.ToArray();
        }

        public DataTable Fetch(string startCell, bool includeTitle, Type[] colTypes)
        {
            var pos = GetCellPos(startCell);
            return Fetch(pos, includeTitle, colTypes);
        }
        public DataTable Fetch((int row, int col) pos, bool includeTitle, Type[] colTypes)
        {
            var ret = new DataTable();
            ret.Columns.Self(_ =>
                _.AddRange(colTypes.Select(colType => new DataColumn("", colType)).ToArray()));

            (int row, int col) dataStart;
            if (includeTitle)
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

    }
}