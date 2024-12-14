using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;

namespace Cvl.DynamicForms.Core.Models.Layouts
{
    public class Legend : ContainerControl
    {
        public Legend(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Legend;
        }
    }

    public class LegendParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Legend(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "legend";
        }
    }
}
