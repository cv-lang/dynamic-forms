using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Base
{
    /// <summary>
    /// 
    /// </summary>
    
    public enum PropertyTypes
    {
        String,
        Int,
        Float,
        Bool,
        Enum,
        Other
    }

    public class PropertyViewModel
    {
        public PropertyTypes Type { get; set; }

        /// <summary>
        /// Nazwa propercji lub ścieżka do propercji 
        /// np. Name
        /// Person.Name
        /// </summary>
        public string BindingPath { get; set; }

        /// <summary>
        /// Label
        /// </summary>
        public string Header { get; set; }

        public string Description { get; set; }

        public string ToolTip { get; set; }
        public object Value { get; set; }
        public int Order { get; internal set; }
        public string Group { get; internal set; }
    }
}
