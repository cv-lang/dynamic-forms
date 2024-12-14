using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;

namespace Cvl.DynamicForms.Core.Models.Layouts
{   
    public class Section : ContainerControl
    {
        public Section(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Section;
        }
    }

    public class SectionParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Section(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return new string[] { "section", "sekcja" }.Contains(name.ToLower());
        }
    }
}
