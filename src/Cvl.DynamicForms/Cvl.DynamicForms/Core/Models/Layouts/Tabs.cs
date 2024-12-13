using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;

namespace Cvl.DynamicForms.Core.Models.Layouts
{
    public class Tabs : ContainerControl
    {
        public Tabs(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
        }
    }

    public class TabsParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Tabs(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "tabs";
        }
    }
}
