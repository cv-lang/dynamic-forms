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
    public class Stack : ContainerControl
    {
        public Stack()
        { }

        public Stack(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
        }
    }

    public class StackParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Stack(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "stack";
        }
    }
}