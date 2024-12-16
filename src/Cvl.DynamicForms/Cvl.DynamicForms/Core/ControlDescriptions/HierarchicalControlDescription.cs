using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.ControlDescriptions
{
    public class HierarchicalControlDescription
    {
        public required string HierarchicalControlType { get; set; }
        public string? ElementName { get; set; }
        public string? TypeName { get; set; }
        public int Level { get; internal set; }
        public int Row { get; internal set; }
    }
}
