using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using System.Collections;
using System.IO;

namespace Dawnx.NPOI
{
    public partial class ExcelBook : IWorkbook
    {
        public int ActiveSheetIndex => MapedWorkbook.ActiveSheetIndex;

        public int FirstVisibleTab { get => MapedWorkbook.FirstVisibleTab; set => MapedWorkbook.FirstVisibleTab = value; }

        public int NumberOfSheets => MapedWorkbook.NumberOfSheets;

        public short NumberOfFonts => MapedWorkbook.NumberOfFonts;

        public short NumCellStyles => MapedWorkbook.NumCellStyles;

        public int NumberOfNames => MapedWorkbook.NumberOfNames;

        public MissingCellPolicy MissingCellPolicy { get => MapedWorkbook.MissingCellPolicy; set => MapedWorkbook.MissingCellPolicy = value; }
        public bool IsHidden { get => MapedWorkbook.IsHidden; set => MapedWorkbook.IsHidden = value; }

        public int AddPicture(byte[] pictureData, PictureType format)
        {
            return MapedWorkbook.AddPicture(pictureData, format);
        }

        public void AddToolPack(UDFFinder toopack)
        {
            MapedWorkbook.AddToolPack(toopack);
        }

        ISheet IWorkbook.CloneSheet(int sheetNum)
        {
            return MapedWorkbook.CloneSheet(sheetNum);
        }
        public ExcelSheet CloneSheet(string name)
        {
            return CloneSheet(GetSheetIndex(name));
        }
        public ExcelSheet CloneSheet(int sheetNum)
            => new ExcelSheet(this, MapedWorkbook.CloneSheet(sheetNum));

        public void Close()
        {
            MapedWorkbook.Close();
        }

        ICellStyle IWorkbook.CreateCellStyle()
        {
            return MapedWorkbook.CreateCellStyle();
        }

        IDataFormat IWorkbook.CreateDataFormat()
        {
            return MapedWorkbook.CreateDataFormat();
        }

        public IFont CreateFont()
        {
            return MapedWorkbook.CreateFont();
        }

        public IName CreateName()
        {
            return MapedWorkbook.CreateName();
        }

        ISheet IWorkbook.CreateSheet()
        {
            return MapedWorkbook.CreateSheet();
        }
        public ExcelSheet CreateSheet()
            => new ExcelSheet(this, MapedWorkbook.CreateSheet());

        ISheet IWorkbook.CreateSheet(string sheetname)
        {
            return MapedWorkbook.CreateSheet(sheetname);
        }
        public ExcelSheet CreateSheet(string sheetname)
            => new ExcelSheet(this, MapedWorkbook.CreateSheet(sheetname));

        public IFont FindFont(short boldWeight, short color, short fontHeight, string name, bool italic, bool strikeout, FontSuperScript typeOffset, FontUnderlineType underline)
        {
            return MapedWorkbook.FindFont(boldWeight, color, fontHeight, name, italic, strikeout, typeOffset, underline);
        }

        public IList GetAllPictures()
        {
            return MapedWorkbook.GetAllPictures();
        }

        public ICellStyle GetCellStyleAt(short idx)
        {
            return MapedWorkbook.GetCellStyleAt(idx);
        }

        public ICreationHelper GetCreationHelper()
        {
            return MapedWorkbook.GetCreationHelper();
        }

        public IEnumerator GetEnumerator()
        {
            return MapedWorkbook.GetEnumerator();
        }

        public IFont GetFontAt(short idx)
        {
            return MapedWorkbook.GetFontAt(idx);
        }

        public IName GetName(string name)
        {
            return MapedWorkbook.GetName(name);
        }

        public IName GetNameAt(int nameIndex)
        {
            return MapedWorkbook.GetNameAt(nameIndex);
        }

        public int GetNameIndex(string name)
        {
            return MapedWorkbook.GetNameIndex(name);
        }

        public string GetPrintArea(int sheetIndex)
        {
            return MapedWorkbook.GetPrintArea(sheetIndex);
        }

        public ExcelSheet GetSheet(string name)
            => new ExcelSheet(this, MapedWorkbook.GetSheet(name));
        ISheet IWorkbook.GetSheet(string name)
        {
            return MapedWorkbook.GetSheet(name);
        }

        ISheet IWorkbook.GetSheetAt(int index)
        {
            return MapedWorkbook.GetSheetAt(index);
        }
        public ExcelSheet GetSheetAt(int index)
            => new ExcelSheet(this, MapedWorkbook.GetSheetAt(index));

        public int GetSheetIndex(string name)
        {
            return MapedWorkbook.GetSheetIndex(name);
        }

        public int GetSheetIndex(ISheet sheet)
        {
            return MapedWorkbook.GetSheetIndex(sheet);
        }
        public int GetSheetIndex(ExcelSheet sheet) => GetSheetIndex(sheet.SheetName);

        public string GetSheetName(int sheet)
        {
            return MapedWorkbook.GetSheetName(sheet);
        }

        public bool IsSheetHidden(int sheetIx)
        {
            return MapedWorkbook.IsSheetHidden(sheetIx);
        }

        public bool IsSheetVeryHidden(int sheetIx)
        {
            return MapedWorkbook.IsSheetVeryHidden(sheetIx);
        }

        public int LinkExternalWorkbook(string name, IWorkbook workbook)
        {
            return MapedWorkbook.LinkExternalWorkbook(name, workbook);
        }

        public void RemoveName(int index)
        {
            MapedWorkbook.RemoveName(index);
        }

        public void RemoveName(string name)
        {
            MapedWorkbook.RemoveName(name);
        }

        public void RemovePrintArea(int sheetIndex)
        {
            MapedWorkbook.RemovePrintArea(sheetIndex);
        }

        public void RemoveSheetAt(int index)
        {
            MapedWorkbook.RemoveSheetAt(index);
        }

        public void SetActiveSheet(int sheetIndex)
        {
            MapedWorkbook.SetActiveSheet(sheetIndex);
        }

        public void SetPrintArea(int sheetIndex, string reference)
        {
            MapedWorkbook.SetPrintArea(sheetIndex, reference);
        }

        public void SetPrintArea(int sheetIndex, int startColumn, int endColumn, int startRow, int endRow)
        {
            MapedWorkbook.SetPrintArea(sheetIndex, startColumn, endColumn, startRow, endRow);
        }

        public void SetRepeatingRowsAndColumns(int sheetIndex, int startColumn, int endColumn, int startRow, int endRow)
        {
#pragma warning disable CS0618
            MapedWorkbook.SetRepeatingRowsAndColumns(sheetIndex, startColumn, endColumn, startRow, endRow);
#pragma warning restore CS0618
        }

        public void SetSelectedTab(int index)
        {
            MapedWorkbook.SetSelectedTab(index);
        }

        public void SetSheetHidden(int sheetIx, SheetState hidden)
        {
            MapedWorkbook.SetSheetHidden(sheetIx, hidden);
        }

        public void SetSheetHidden(int sheetIx, int hidden)
        {
            MapedWorkbook.SetSheetHidden(sheetIx, hidden);
        }

        public void RenameSheet(string oldName, string newName)
        {
            SetSheetName(GetSheetIndex(oldName), newName);
        }

        public void SetSheetName(int sheet, string name)
        {
            MapedWorkbook.SetSheetName(sheet, name);
        }

        public void SetSheetOrder(string sheetname, int pos)
        {
            MapedWorkbook.SetSheetOrder(sheetname, pos);
        }

        public void Write(Stream stream)
        {
            MapedWorkbook.Write(stream);
        }
    }
}
