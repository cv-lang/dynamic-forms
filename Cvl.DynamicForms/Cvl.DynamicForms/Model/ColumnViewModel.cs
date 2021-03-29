using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.DynamicForms.Model
{
    public class ColumnViewModel
    {
        /// <summary>
        /// Nazwa propercji lub ścieżka do propercji 
        /// np. Name
        /// Person.Name
        /// </summary>
        public string BindingPath { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }

        public string ToolTip { get; set; }
    }
}
