using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;

namespace Cvl.DynamicForms.Core.Models.Layouts
{   
    public class Grid : ContainerControl
    {
        public Grid(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Grid;
        }
    }

    public class GridParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Grid(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "grid";
        }
    }
}
