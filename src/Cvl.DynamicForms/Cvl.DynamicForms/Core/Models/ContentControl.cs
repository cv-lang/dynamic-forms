using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models
{
    public class ContentControl : Control
    {
        public ContentControlType Type { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }
        public string? DataSource { get; set; }
        public string? Placeholder { get; set; }
        public string? Validation { get; set; }
        public string? ValidationMessage { get; set; }
        public string? Tooltip { get; set; }
        public string? Value { get; set; }
        public string? Binding { get; set; }
        public bool? IsRequired { get; set; }
        public string? Action { get; set; }
        public string? Notes { get; set; }

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
    }

    public enum ContentControlType
    {
        Auto,
        Legend,
        Text,
        Date,
        Checkbox,
        Button,
        Combo,
        Icon,
        Info,
        Currency
    }
}
