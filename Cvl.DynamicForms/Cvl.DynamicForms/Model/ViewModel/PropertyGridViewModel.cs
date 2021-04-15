using System.Collections.Generic;

namespace Cvl.DynamicForms.Model
{
    public class PropertyBaseVM
    {
        /// <summary>
        /// Label
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Nazwa propercji obiektu (zawsze bez .) - pojedyńcza konkretna property
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Nazwa propercji lub ścieżka do propercji (od rodzica 
        /// np. Name
        /// Person.Name
        /// </summary>
        public string BindingPath { get; set; }

        public string PropertyUniqueName => BindingPath?.Replace(".", "_") ?? PropertyName;
        public string PropertyValue { get; set; }        

        public PropertyTypes Type { get; set; }       

        public string Description { get; set; }
        public object Value { get; set; }
        public int Order { get; internal set; }
    }

    /// <summary>
    /// 
    /// </summary>

    public enum PropertyTypes
    {
        String,
        StringEditor,
        StringXml,
        Int,
        Float,
        Bool,
        Enum,
        Class,
        Collection,
        Other
    }

    public class SimplePropertyVM : PropertyBaseVM
    {
        public string Group { get; internal set; }
    }

    public class RegionVM : PropertyBaseVM
    {
        //znaczy że propertyGrid ma być w całości wygnerowany na serwerze (bez ajaxa)
        public bool IsStatic { get; set; }

        public string EditUrl { get; set; }

        public string MainObjectId { get; internal set; }
        public string MainObjectTypeFullname { get; internal set; }
    }

    /// <summary>
    /// Grupa propertisów wewnątrz propertyGrida
    /// </summary>
    public class GroupVM : RegionVM
    {        
        public List<PropertyBaseVM> Properties { get; set; } = new List<PropertyBaseVM>();
    }

    /// <summary>
    /// Widok częściowy property grid - pod kontolka używana do wyświetlania potomków
    /// </summary>
    public class PropertyGridVM : GroupVM
    {      
        
        public string ListUrl { get; internal set; }
        
    }    

    
}
