﻿using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.ContentControls;
using Cvl.DynamicForms.Core.Models.Layouts;
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
