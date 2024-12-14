using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Layouts
{
    public class Footer : ContainerControl
    {
        public Footer(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Footer;
        }
    }

    public class FooterParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Footer(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "footer";
        }
    }
}