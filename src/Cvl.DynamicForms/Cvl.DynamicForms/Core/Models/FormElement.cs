using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models
{
    public class FormElement
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public FormElementType Type { get; set; }
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
    }

    public enum FormElementType
    {
        Auto,
        Legend,

        Text,
        Date,
        Bool,
        Button,
        Combo,
        Icon,
        Info,
        Currency
    }
}
