using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.DynamicForms.Model.ViewModel
{
    public class GridViewModel
    {
        public List<ColumnViewModel> Columns { get; set; } = new List<ColumnViewModel>();

        public List<RowViewModel> Rows { get; set; } = new List<RowViewModel>();
        public string PropertyValue { get; internal set; }
    }
}
