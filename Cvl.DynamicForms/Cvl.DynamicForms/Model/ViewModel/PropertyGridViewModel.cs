using System.Collections.Generic;

namespace Cvl.DynamicForms.Model
{
    public class PropertyBaseViewModel
    {

    }


    /// <summary>
    /// Widok kontrolki property grid - całej
    /// </summary>
    public class PropertyGridViewModel : PropertyGridElementViewModel
    {
        public string ObjectTypeName { get; set; }
        public string ObjectId { get; set; }
        public string ListUrl { get; internal set; }
    }

    /// <summary>
    /// Widok częściowy property grid - pod kontolka używana do wyświetlania potomków
    /// </summary>
    public class PropertyGridElementViewModel : PropertyBaseViewModel
    {
        public string PropertyName { get; set; }
        public string PropertyUniqueName { get; set; }
        public string PropertyValue { get; set; }

        public List<PropertyGroupViewModel> Groups { get; set; } = new List<PropertyGroupViewModel>();
        public string EditUrl { get; set; }
        public string BindingPath { get; set; }

        //znaczy że propertyGrid ma być w całości wygnerowany na serwerze (bez ajaxa)
        public bool IsStatic { get;  set; }
    }    

    /// <summary>
    /// Grupa propertisów wewnątrz propertyGrida
    /// </summary>
    public class PropertyGroupViewModel
    {
        public List<PropertyBaseViewModel> Properties { get; set; } = new List<PropertyBaseViewModel>();
    }
}
