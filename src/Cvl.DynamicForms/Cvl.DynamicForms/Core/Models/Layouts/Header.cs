using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;

namespace Cvl.DynamicForms.Core.Models.Layouts
{
    public class Header : ContainerControl
    {
        public Header(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
        }
    }

    public class HeaderParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Layout(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "header";
        }
    }
}