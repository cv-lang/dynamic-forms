using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.ContentControls;
using Cvl.DynamicForms.Core.Models.Layouts;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Base
{
    /// <summary>
    /// Kontrolka prosta typu - textbox, checkbox, autocomplet itp
    /// </summary>
    
    public abstract class ContentControl : Control
    {        
        public ContentControl(ControlDescription controlDescription) 
        {
            Id = controlDescription.Row.ToString();
            Name = controlDescription.ElementName == "" ? this.GetType().Name : controlDescription.ElementName;
            Icon = controlDescription.Icon;
            Column = controlDescription.Column;
            Style = controlDescription.Style;
            ElementStyle = controlDescription.ElementStyle;
            Min = controlDescription.Min;
            Max = controlDescription.Max;
            Format = controlDescription.Format;
            Width = controlDescription.Width;
            Height = controlDescription.Height;
            TestDataRow1 = controlDescription.TestDataRow1;
            TestDataRow2 = controlDescription.TestDataRow2;
            TestDataRow3 = controlDescription.TestDataRow3;
            TestDataRow4 = controlDescription.TestDataRow4;
            TestDataRow5 = controlDescription.TestDataRow5;           
        }

        public string? Placeholder { get; set; }
        public string? Validation { get; set; }
        public string? ValidationMessage { get; set; }
        public string? Tooltip { get; set; }
        public string? Value { get; set; }
        public string? Binding { get; set; }
        public bool? IsRequired { get; set; }
        public bool IsReadOnly { get; set; } = false;
        public string? Action { get; set; }
        public string? Notes { get; set; }
        public string? Icon { get; set; }
        public string? Min { get; set; }
        public string? Max { get; set; }
        public string? Format { get; set; }
        public string? Width { get; private set; }
        public string? Height { get; private set; }
        public string? Column {  get; set; }
        public string? Style { get; set; }
        public string? ElementStyle { get; set; }
        public string? TestDataRow1 { get; set; }
        public string? TestDataRow2 { get; set; }
        public string? TestDataRow3 { get; set; }
        public string? TestDataRow4 { get; set; }
        public string? TestDataRow5 { get; set; }

        public ControlValues ControlValues { get; set; } = new();

        public override string ToString()
        {
            return $"{Name} - {Type}";
        }
    }

    public class ControlValues
    {
        public bool? BoolValue { get; set; }
        public string? StringValue { get; set; }
        public decimal DecimalValue { get; set; }
    }

}
