using Cvl.DynamicForms.Core.ControlDescriptions;
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
    


    public class ExcelRowReader
    {
        public HierarchicalControlDescription ReadHierarchicalControlRow(IWorksheet ws, int row, int level)
        {
            var elementName = ws.GetCellText(row, ExcelColumnIndex.Name) ?? "";

            var excelRow = new HierarchicalControlDescription()
            {
                Name = ws.GetCellText(row, level),
                ElementName = elementName,
                TypeName = ws.GetCellText(row, ExcelColumnIndex.Type)?.Trim() ?? "",
                Level = level,
                Row = row,
            };
            return excelRow;
        }

        public ControlDescription ReadControlRow(IWorksheet ws, int row, int level)
        {          
            var isRequiredString = ws.GetCellText(row, ExcelColumnIndex.IsRequired);
            var isReadOnlyString = ws.GetCellText(row, ExcelColumnIndex.IsReadOnly);

            var excelRow = new ControlDescription()
            {
                Row = row,
                ElementName = ws.GetCellText(row, ExcelColumnIndex.Name) ?? "",
                TypeName = ws.GetCellText(row, ExcelColumnIndex.Type)?.Trim() ?? "",
                IsRequired = isRequiredString == "1",
                IsReadOnly = isReadOnlyString == "1",
                Description = ws.GetCellText(row, ExcelColumnIndex.Description) ?? "",
                Datasource = ws.GetCellText(row, ExcelColumnIndex.Datasource) ?? "",
                Icon = ws.GetCellText(row, ExcelColumnIndex.Icon) ?? "",
                Placeholder = ws.GetCellText(row, ExcelColumnIndex.Placeholder) ?? "",
                Aligment = ws.GetCellText(row, ExcelColumnIndex.Aligment) ?? "",
                Validation = ws.GetCellText(row, ExcelColumnIndex.Validation) ?? "",
                ValidationMessage = ws.GetCellText(row, ExcelColumnIndex.ValidationMessage) ?? "",
                Tooltip = ws.GetCellText(row, ExcelColumnIndex.Tooltip) ?? "",
                Value = ws.GetCellText(row, ExcelColumnIndex.Value) ?? "",
                Binding = ws.GetCellText(row, ExcelColumnIndex.Binding) ?? "",
                SelectedElementBinding = ws.GetCellText(row, ExcelColumnIndex.SelectedElementBinding) ?? "",
                Action = ws.GetCellText(row, ExcelColumnIndex.Action) ?? "",
                Comments = ws.GetCellText(row, ExcelColumnIndex.Comments) ?? "",
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
        public const int Aligment = 15;
        public const int Validation = 16;
        public const int ValidationMessage = 17;
        public const int Tooltip = 18;
        public const int Value = 19;
        public const int Binding = 20;
        public const int SelectedElementBinding = 21;
        public const int Action = 22;
        public const int Comments = 23;
    }
}
