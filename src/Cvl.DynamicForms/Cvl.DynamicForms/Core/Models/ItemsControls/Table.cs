using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;

namespace Cvl.DynamicForms.Core.Models.Layouts
{
    public class Table : ContainerControl
    {
        public Table(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Table;
        }        
    }

    public class TableParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Table(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return new string[] { "table", "tabela" }.Contains(name.ToLower());
        }
    }
}
