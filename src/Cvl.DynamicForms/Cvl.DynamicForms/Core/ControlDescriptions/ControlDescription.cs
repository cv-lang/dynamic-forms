using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.ControlDescriptions
{
    public class ControlDescription
    {
        public required string ElementName { get; set; }
        public required string ElementType { get; set; }
        public required string Placeholder { get; set; }
        public required bool IsRequired { get; set; }
        public required string Description { get; set; }
        public required string Datasource { get; set; }
        public required string Binding { get; set; }
        public bool IsReadOnly { get; internal set; }
        public required string Icon { get; set; }
        public required string Aligment { get; set; }
        public required string Validation { get; set; }
        public required string ValidationMessage { get; set; }
        public required string Tooltip { get; set; }
        public required string Value { get; set; }
        public required string SelectedElementBinding { get; set; }
        public required string Action { get; set; }
        public required string Comments { get; set; }
        public string? Column { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public string? Style { get; set; }
        public string? ElementStyle { get; set; }
        public string? Min { get; set; }
        public string? Max { get; set; }
        public string? Format { get; set; }
        public string? TestDataRow1 { get; set; }
        public string? TestDataRow2 { get; set; }
        public string? TestDataRow3 { get; set; }
        public string? TestDataRow4 { get; set; }
        public string? TestDataRow5 { get; set; }
        public int Row { get; internal set; }
    }
}
