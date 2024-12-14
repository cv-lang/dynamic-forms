using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;

namespace Cvl.DynamicForms.Core.Models.Layouts
{    
    public class Tab : ContainerControl
    {
        public Tab(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Tab;
        }
    }

    public class TabParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Tab(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "tab";
        }
    }
}
