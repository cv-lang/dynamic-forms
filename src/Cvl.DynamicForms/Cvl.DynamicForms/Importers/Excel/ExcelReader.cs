using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Importers.Excel
{
    public interface IExcelReader
    {
        public IWorksheet GetWorksheet(string filePath, int sheetIndex);
    }

    public interface IWorksheet
    {
        public string Name { get; }
        public string? GetCellText(int rowIndex, int columnIndex);
        public object? GetCellValue(int rowIndex, int columnIndex);
        public T? GetCellValue<T>(int rowIndex, int columnIndex) where T : struct;
    }


    internal class ExcelReader : IExcelReader
    {
        public IWorksheet GetWorksheet(string filePath, int sheetIndex)
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            ExcelWorksheet sheet = package.Workbook.Worksheets[sheetIndex];
            var ws = new Worksheet(sheet);

            return ws;
        }
    }

    internal class Worksheet : IWorksheet
    {
        private ExcelWorksheet _sheet;

        public Worksheet(ExcelWorksheet sheet)
        {
            _sheet = sheet;
        }

        public string Name => _sheet.Name;

        public string? GetCellText(int rowIndex, int columnIndex)
        {
            var val = _sheet.Cells[rowIndex, columnIndex].Text?.Trim();

            if (string.IsNullOrEmpty(val)) return null;
            return val;
        }

        public object? GetCellValue(int rowIndex, int columnIndex)
        {
            var val = _sheet.Cells[rowIndex, columnIndex].Value;
            return val;
        }

        public T? GetCellValue<T>(int rowIndex, int columnIndex) where T : struct
        {
            var val = GetCellText(rowIndex, columnIndex);
            if (string.IsNullOrEmpty(val))
                return null;

            if (typeof(T) == typeof(DateTime))
            {
                return (T)(object)DateTime.Parse(val, CultureInfo.GetCultureInfo("PL-pl")).ToUniversalTime();
            }
            else if (typeof(T) == typeof(long))
            {
                return (T)(object)long.Parse(val.Replace(" ", ""));
            }
            else if (typeof(T) == typeof(decimal))
            {
                return (T)(object)decimal.Parse(val.Replace(" ", ""));
            }

            return null;
        }
    }
}
