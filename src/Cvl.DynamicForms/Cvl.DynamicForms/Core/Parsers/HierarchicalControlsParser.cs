using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Models.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Parsers
{
    public interface IHierarchicalControlParser
    {
        bool IsHierarhicalControl(string name);
        HierarchicalControl Create(HierarchicalControlDescription hierarchicalControlDescription);
    }

    public class HierarchicalControlsParser
    {
        private static IHierarchicalControlParser[] parsers = { new LayoutParser(), new HeaderParser(), new FooterParser(),
            new StackParser(), new RowParser(), new ColumnParser(), new SidebarParser() };

        internal bool IsHierarhicalControl(HierarchicalControlDescription hierarchicalControlDescription)
        {
            return parsers.Any(x => x.IsHierarhicalControl(hierarchicalControlDescription.Name));
        }

        internal HierarchicalControl Create(HierarchicalControlDescription hierarchicalControlDescription)
        {
            return parsers.First(x => x.IsHierarhicalControl(hierarchicalControlDescription.Name))
                .Create(hierarchicalControlDescription);
        }
    }
}
