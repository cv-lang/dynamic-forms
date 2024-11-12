using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Layouts
{
    public class Layout : HierarchicalControl
    {
    }

    public class LayoutParser : IHierarchicalControlParser
    {
        public HierarchicalControl Create(HierarchicalControlDescription description)
        {
            return new Layout()
            {
                Id = description.Row.ToString(),
                Name = description.Name
            };
        }

        public bool IsHierarhicalControl(string name)
        {
            return name.ToLower() == "layout";
        }
    }
}
