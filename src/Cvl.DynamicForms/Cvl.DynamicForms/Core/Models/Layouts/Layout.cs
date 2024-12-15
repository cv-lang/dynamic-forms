﻿using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;

namespace Cvl.DynamicForms.Core.Models.Layouts
{
    public class Layout : ContainerControl
    {
        public Layout(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Layout;
        }
    }

    public class LayoutParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Layout(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "layout";
        }
    }
}