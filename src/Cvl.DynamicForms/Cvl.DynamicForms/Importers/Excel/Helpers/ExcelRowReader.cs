using Cvl.DynamicForms.Importers.Excel;
using Cvl.DynamicForms.Importers.Excel.Tools;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Importers.Excel.Helpers
{
    public class ExcelRow
    {
        public required string ControlName { get; set; }
        public required string ElementName { get; set; }
        public required string Placeholder { get; set; }
        public required string ElementValue { get; set; }
        public required bool IsRequired { get; set; }
        public required string Description { get; set; }
        public required string Datasource { get; set; }
        public required string Binding { get; set; }
        public required string StringType { get; set; }
    }


    public class ExcelRowReader
    {
        public ExcelRow ReadRow(IWorksheet ws, int row, int level)
        {
            var isRequiredString = ws.GetCellText(row, ExcelColumnIndex.IsRequired);
            var elementName = ws.GetCellText(row, ExcelColumnIndex.Name) ?? "";

            var excelRow = new ExcelRow()
            {
                ControlName = ws.GetCellText(row, level),
                ElementName = elementName,
                ElementValue = ws.GetCellText(row, ExcelColumnIndex.Value),
                Placeholder = ws.GetCellText(row, ExcelColumnIndex.Placeholder) ?? "",
                IsRequired = isRequiredString == "1",
                Description = ws.GetCellText(row, ExcelColumnIndex.Description) ?? "",
                Datasource = ws.GetCellText(row, ExcelColumnIndex.Datasource) ?? "",
                Binding = ws.GetCellText(row, ExcelColumnIndex.Binding) ??
                    elementName.Replace(" ", "_"),
                StringType = ws.GetCellText(row, ExcelColumnIndex.Type)?.Trim() ?? "tekst"
            };
            return excelRow;
        }
    }
    public class ExcelColumnIndex
    {
        public const int Name = 7;
        public const int Type = 8;
        public const int IsRequired = 9;
        public const int IsReadOnly = 10;
        public const int Description = 11;
        public const int Datasource = 12;
        public const int Icon = 13;
        public const int Placeholder = 14;
        public const int Tooltip = 18;
        public const int Value = 19;
        public const int Binding = 20;
        
        public const int Action = 22;
    }
}
