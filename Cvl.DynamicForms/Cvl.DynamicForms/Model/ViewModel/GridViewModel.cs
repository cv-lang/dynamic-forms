using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.DynamicForms.Model.ViewModel
{
    public class GridVM : RegionVM
    {
        public List<ColumnViewModel> Columns { get; set; } = new List<ColumnViewModel>();

        public List<RowViewModel> Rows { get; set; } = new List<RowViewModel>();
        public string ObjectTypeName { get; set; }
        public string ObjectId { get; set; }
    }
}
