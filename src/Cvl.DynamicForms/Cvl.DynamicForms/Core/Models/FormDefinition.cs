using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.DynamicForms.Core.Models.Base;

namespace Cvl.DynamicForms.Core.Models
{
    public class FormDefinition
    {
        public required string Name { get; set; }
        public required HierarchicalControl RootSection { get; set; }
    }
}
