﻿using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Layouts
{
    public class Column : ContainerControl
    {
        public Column(HierarchicalControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Column;
        }
    }

    public class ColumnParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Column(description);
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "column";
        }
    }
}