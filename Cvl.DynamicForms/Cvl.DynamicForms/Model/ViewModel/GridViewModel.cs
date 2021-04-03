using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.DynamicForms.Model.ViewModel
{
    public class GridViewModel : PropertyBaseViewModel
    {
        public List<ColumnViewModel> Columns { get; set; } = new List<ColumnViewModel>();

        public List<RowViewModel> Rows { get; set; } = new List<RowViewModel>();
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string EditUrl { get; internal set; }
    }
}
