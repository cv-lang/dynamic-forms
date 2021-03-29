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
    public class PropertyViewModel
    {
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
    }
}
