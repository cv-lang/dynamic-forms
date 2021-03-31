using System.Collections.Generic;

namespace Cvl.DynamicForms.Model
{
    public class PropertyBaseViewModel
    {

    }

    public class PropertyGridViewModel : PropertyGridElementViewModel
    {
        public string ObjectTypeName { get; set; }
        public string ObjectId { get; set; }
    }

    public class PropertyGridElementViewModel : PropertyBaseViewModel
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

        public List<PropertyGroupViewModel> Groups { get; set; } = new List<PropertyGroupViewModel>();
    }

    public class GridElementViewModel : PropertyBaseViewModel
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

        //nagłówek
        public List<ColumnViewModel> Columns { get; set; } = new List<ColumnViewModel>();

        public List<RowViewModel> Rows { get; set; } = new List<RowViewModel>();
    }

    public class PropertyGroupViewModel
    {
        public List<PropertyBaseViewModel> Properties { get; set; } = new List<PropertyBaseViewModel>();
    }
}
