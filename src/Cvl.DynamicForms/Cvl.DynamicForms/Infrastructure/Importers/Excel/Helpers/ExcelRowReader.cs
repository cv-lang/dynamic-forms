using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Infrastructure.Importers.Excel.Tools;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Infrastructure.Importers.Excel.Helpers
{



    public class ExcelRowReader
    {
        public HierarchicalControlDescription ReadHierarchicalControlRow(IWorksheet ws, int row, int level)
        {
            var elementName = ws.GetCellText(row, ExcelColumnIndex.ElementName) ?? "";

            var excelRow = new HierarchicalControlDescription()
            {
                HierarchicalControlType = ws.GetCellText(row, level),
                ElementName = elementName,
                TypeName = ws.GetCellText(row, ExcelColumnIndex.ElementType)?.Trim() ?? ws.GetCellText(row, level) ?? "",
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
                ElementName = ws.GetCellText(row, ExcelColumnIndex.ElementName) ?? "",
                ElementType = ws.GetCellText(row, ExcelColumnIndex.ElementType)?.Trim() ?? "",
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
                Column = ws.GetCellText(row, ExcelColumnIndex.Column) ,
                Style = ws.GetCellText(row, ExcelColumnIndex.Style),
                ElementStyle = ws.GetCellText(row, ExcelColumnIndex.ElementStyle),
                Min = ws.GetCellText(row, ExcelColumnIndex.Min),
                Max = ws.GetCellText(row, ExcelColumnIndex.Max),
                Format = ws.GetCellText(row, ExcelColumnIndex.Format),
                Width = ws.GetCellText(row, ExcelColumnIndex.Width),
                Height = ws.GetCellText(row, ExcelColumnIndex.Height),
                TestDataRow1 = ws.GetCellText(row, ExcelColumnIndex.TestDataRow1),
                TestDataRow2 = ws.GetCellText(row, ExcelColumnIndex.TestDataRow2),
                TestDataRow3 = ws.GetCellText(row, ExcelColumnIndex.TestDataRow3),
                TestDataRow4 = ws.GetCellText(row, ExcelColumnIndex.TestDataRow4),
                TestDataRow5 = ws.GetCellText(row, ExcelColumnIndex.TestDataRow5),
            };
            return excelRow;
        }
    }
    public class ExcelColumnIndex
    {
        public const int ElementName = 7;
        public const int ElementType = 8;
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
        public const int Min = 24;
        public const int Max = 25;
        public const int Format = 26;
        public const int Width = 27;
        public const int Height = 28;
        public const int Column = 29;
        public const int Style = 30;
        public const int ElementStyle = 31;
        public const int TestDataRow1 = 32;
        public const int TestDataRow2 = 33;
        public const int TestDataRow3 = 34;
        public const int TestDataRow4 = 35;
        public const int TestDataRow5 = 36;

    }
}
